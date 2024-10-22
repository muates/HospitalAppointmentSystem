using HospitalAppointmentSystem.Core.Model.Response;
using HospitalAppointmentSystem.Core.Service.Abstract;
using HospitalAppointmentSystem.Model.Dto.Patient.Request;
using HospitalAppointmentSystem.Model.Entity;

namespace HospitalAppointmentSystem.Application.Service.Abstract;

public interface IPatientService : IGenericService<Patient, int>
{
    Task<OperationResponse<object>> AddAsync(PatientCreateRequest request);
    Task<OperationResponse<object>> UpdateAsync(int id, PatientUpdateRequest request);
}