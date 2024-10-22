using System.Transactions;
using HospitalAppointmentSystem.Application.Converter;
using HospitalAppointmentSystem.Application.Service.Abstract;
using HospitalAppointmentSystem.Core.Model.Response;
using HospitalAppointmentSystem.Core.Service.Concrete;
using HospitalAppointmentSystem.DataAccess.Repository.Abstract;
using HospitalAppointmentSystem.Model.Dto.Appointment.Request;
using HospitalAppointmentSystem.Model.Entity;
using HospitalAppointmentSystem.Model.Enum;

namespace HospitalAppointmentSystem.Application.Service.Concrete;

public class AppointmentService(
    IAppointmentRepository repository,
    IDoctorService doctorService,
    IPatientService patientService)
    : GenericService<IAppointmentRepository, Appointment, string>(repository), IAppointmentService
{
    private readonly IAppointmentRepository _repository = repository;
    private readonly IDoctorService _doctorService = doctorService;
    private readonly IPatientService _patientService = patientService;

    public new async Task<OperationResponse<Appointment>> GetByIdAsync(string id)
    {
        var appointment = await _repository.GetByIdAsync(id);
        
        if (appointment is null) 
            return new OperationResponse<Appointment>(404, "Appointment not found", null);

        var currentDate = DateTime.UtcNow;
        
        if (appointment.AppointmentDate < currentDate && 
            appointment.Status != AppointmentStatus.Canceled &&
            appointment.Status != AppointmentStatus.Completed)
        {
            appointment.Status = AppointmentStatus.Canceled;
            await _repository.UpdateAsync(appointment);

            return new OperationResponse<Appointment>(404, "Appointment date cannot be in the future", null);
        }
        
        return new OperationResponse<Appointment>(200, "Appointment retrieved successfully", appointment);
    }

    public async Task<OperationResponse<object>> AddAsync(AppointmentCreateRequest request)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        try
        {
            var hasSlots = await HasAvailableAppointmentSlotsAsync(request.DoctorId);
            if (!hasSlots)
            {
                return new OperationResponse<object>(400, "Doctor does not have available appointment slots", null);
            }

            var appointment = AppointmentConverter.ToEntity(request);

            var (existDoctor, existPatient) = await GetDoctorAndPatientAsync(request.DoctorId, request.PatientId);

            if (existDoctor.Patients.All(p => p.Id != existPatient.Id))
            {
                existDoctor.Patients.Add(existPatient);
                await _doctorService.UpdateAsync(existDoctor);
            }

            await _repository.AddAsync(appointment);

            scope.Complete();
            return new OperationResponse<object>(201, "Appointment created successfully", null);
        }
        catch (Exception e)
        {
            return new OperationResponse<object>(500, $"An error occurred while creating the appointment: {e.Message}",
                null);
        }
    }

    public async Task<OperationResponse<object>> UpdateAsync(string id, AppointmentUpdateRequest request)
    {
        var existAppointment = await GetByIdAsync(id);

        AutoMapperService.Map(existAppointment, request);

        try
        {
            if (existAppointment.Data != null) await _repository.UpdateAsync(existAppointment.Data);
            
            return new OperationResponse<object>(200, "Appointment updated successfully", null);
        }
        catch (Exception e)
        {
            return new OperationResponse<object>(500, $"An error occurred while updating the appointment: {e.Message}",
                null);
        }
    }

    public async Task<bool> HasAvailableAppointmentSlotsAsync(int doctorId)
    {
        return await _repository.HasAvailableAppointmentSlotsAsync(doctorId);
    }

    public async Task CancelExpiredAppointmentsAsync()
    {
        var expiredAppointments = await _repository.GetExpiredAppointmentsAsync();
        foreach (var appointment in expiredAppointments)
        {
            appointment.Status = AppointmentStatus.Canceled;
            await _repository.UpdateAsync(appointment);
        }
    }

    private async Task<(Doctor doctor, Patient patient)> GetDoctorAndPatientAsync(int doctorId, int patientId)
    {
        var doctorTask = _doctorService.GetByIdAsync(doctorId);
        var patientTask = _patientService.GetByIdAsync(patientId);
        
        await Task.WhenAll(doctorTask, patientTask);
        
        var doctorResult = await doctorTask;
        var patientResult = await patientTask;
        
        if (doctorResult.Data is null) throw new ApplicationException("Doctor not found");
        if (patientResult.Data is null) throw new NullReferenceException("Patient is null");

        return (doctorResult.Data, patientResult.Data);
    }
}