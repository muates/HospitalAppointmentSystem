using HospitalAppointmentSystem.Application.Converter;
using HospitalAppointmentSystem.Application.Service.Abstract;
using HospitalAppointmentSystem.Core.Model.Response;
using HospitalAppointmentSystem.Core.Service.Concrete;
using HospitalAppointmentSystem.DataAccess.Repository.Abstract;
using HospitalAppointmentSystem.Model.Dto.Patient.Request;
using HospitalAppointmentSystem.Model.Entity;

namespace HospitalAppointmentSystem.Application.Service.Concrete;

public class PatientService(IPatientRepository repository)
    : GenericService<IPatientRepository, Patient, int>(repository), IPatientService
{
    private readonly IPatientRepository _repository = repository;
    
    public async Task<OperationResponse<object>> AddAsync(PatientCreateRequest request)
    {
        try
        {
            var patient = PatientConverter.ToEntity(request);
            
            await _repository.AddAsync(patient);
            
            return new OperationResponse<object>(201, "Patient added successfully", null);
        }
        catch (Exception e)
        {
            return new OperationResponse<object>(500, $"An error occurred while creating the patient: {e.Message}", null);
        }
    }

    public async Task<OperationResponse<object>> UpdateAsync(int id, PatientUpdateRequest request)
    {
        var existPatient = await GetByIdAsync(id);
        
        AutoMapperService.Map(existPatient, request);

        try
        {
            if (existPatient.Data != null) await _repository.UpdateAsync(existPatient.Data);
            
            return new OperationResponse<object>(200, "Patient updated successfully", null);
        }
        catch (Exception e)
        {
            return new OperationResponse<object>(500, $"An error occurred while updating the patient: {e.Message}",
                null);
        }
    }
}