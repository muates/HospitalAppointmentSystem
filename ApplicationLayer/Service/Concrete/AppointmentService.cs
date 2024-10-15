using System.Linq.Expressions;
using System.Transactions;
using HospitalAppointmentSystem.ApplicationLayer.Common.Response;
using HospitalAppointmentSystem.ApplicationLayer.Converter;
using HospitalAppointmentSystem.ApplicationLayer.Dto.Appointment.Request;
using HospitalAppointmentSystem.ApplicationLayer.Service.Abstract;
using HospitalAppointmentSystem.DataAccessLayer.Repository.Abstract;
using HospitalAppointmentSystem.EntityLayer.Enum;
using HospitalAppointmentSystem.EntityLayer.Model;

namespace HospitalAppointmentSystem.ApplicationLayer.Service.Concrete;

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IDoctorService _doctorService;
    private readonly IPatientService _patientService;

    public AppointmentService
    (
        IAppointmentRepository appointmentRepository,
        IDoctorService doctorService,
        IPatientService patientService
    )
    {
        _appointmentRepository = appointmentRepository;
        _doctorService = doctorService;
        _patientService = patientService;
    }

    public async Task<ApiResponse<IEnumerable<Appointment>>> GetAllAsync()
    {
        var appointments = await _appointmentRepository.GetAllAsync();

        var enumerable = appointments as Appointment[] ?? appointments.ToArray();
        
        return enumerable.Length == 0
            ? new ApiResponse<IEnumerable<Appointment>>(404, "No appointments found.", null)
            : new ApiResponse<IEnumerable<Appointment>>(200, "Appointments retrieved successfully.", enumerable);
    }

    public async Task<ApiResponse<Appointment>> GetByIdAsync(string id)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(id);

        if (appointment is null)
            return new ApiResponse<Appointment>(404, "Appointment not found.", null);
        
        var currentDate = DateTime.UtcNow;
        if (appointment.AppointmentDate < currentDate && appointment.Status != AppointmentStatus.Canceled)
        {
            appointment.Status = AppointmentStatus.Canceled;
            _appointmentRepository.Update(appointment);
            
            return new ApiResponse<Appointment>(404, "Appointment date cannot be in the future.", null);
        }

        return new ApiResponse<Appointment>(200, "Appointment retrieved successfully.", appointment);
    }

    public async Task<ApiResponse<IEnumerable<Appointment>>> FindAsync(
        Expression<Func<Appointment, bool>>? predicate = null)
    {
        var appointments = await _appointmentRepository.FindAsync(predicate);
        return new ApiResponse<IEnumerable<Appointment>>(200, "Appointments retrieved successfully.", appointments);
    }

    public async Task<ApiResponse<object>> DeleteAsync(string id)
    {
        await _appointmentRepository.DeleteAsync(id);
        return new ApiResponse<object>(200, "Appointment deleted successfully.", null);
    }

    public async Task<ApiResponse<object>> AddAsync(AppointmentCreateRequest request)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        try
        {
            var hasSlots = await HasAvailableAppointmentSlotsAsync(request.DoctorId);
            if (!hasSlots)
            {
                return new ApiResponse<object>(400, "Doctor does not have available appointment slots.", null);
            }

            var appointment = AppointmentConverter.ToEntity(request);

            var (existDoctor, existPatient) = await GetDoctorAndPatient(request.DoctorId, request.PatientId);

            appointment.DoctorId = existDoctor.Id;
            appointment.PatientId = existPatient.Id;
            
            if (existDoctor.Patients.All(p => p.Id != existPatient.Id))
            {
                existDoctor.Patients.Add(existPatient);
                _doctorService.Update(existDoctor);
            }

            await _appointmentRepository.AddAsync(appointment);

            scope.Complete();
            return new ApiResponse<object>(201, "Appointment created successfully.", null);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
            return new ApiResponse<object>(500, "An error occurred while creating the appointment.", null);
        }
    }

    public void Update(string id, AppointmentUpdateRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> HasAvailableAppointmentSlotsAsync(int doctorId)
    {
        return await _appointmentRepository.HasAvailableAppointmentSlotsAsync(doctorId);
    }

    public async Task CancelExpiredAppointmentsAsync()
    {
        var expiredAppointments = await _appointmentRepository.GetExpiredAppointmentsAsync();
        foreach (var appointment in expiredAppointments)
        {
            appointment.Status = AppointmentStatus.Canceled;
            _appointmentRepository.Update(appointment);
        }
    }

    private async Task<(Doctor doctor, Patient patient)> GetDoctorAndPatient(int doctorId, int patientId)
    {
        var doctorTask = _doctorService.GetByIdAsync(doctorId);
        var patientTask = _patientService.GetByIdAsync(patientId);

        await Task.WhenAll(doctorTask, patientTask);

        var doctorResult = await doctorTask;
        var patientResult = await patientTask;

        if (doctorResult.Data is null) throw new NullReferenceException("Doctor is null");
        if (patientResult.Data is null) throw new NullReferenceException("Patient is null");

        return (doctorResult.Data, patientResult.Data);
    }
}