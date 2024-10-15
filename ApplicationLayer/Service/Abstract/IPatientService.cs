using HospitalAppointmentSystem.ApplicationLayer.Common.Response;
using HospitalAppointmentSystem.ApplicationLayer.Dto.Patient.Request;
using HospitalAppointmentSystem.EntityLayer.Model;

namespace HospitalAppointmentSystem.ApplicationLayer.Service.Abstract;

public interface IPatientService : IGenericService<Patient, int>
{
    Task<ApiResponse<object>> AddAsync(PatientCreateRequest request);
    void Update(int id, PatientUpdateRequest request);
}