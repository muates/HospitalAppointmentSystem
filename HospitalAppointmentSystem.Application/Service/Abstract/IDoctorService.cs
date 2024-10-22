using HospitalAppointmentSystem.Core.Model.Response;
using HospitalAppointmentSystem.Core.Service.Abstract;
using HospitalAppointmentSystem.Model.Dto.Doctor.Request;
using HospitalAppointmentSystem.Model.Entity;

namespace HospitalAppointmentSystem.Application.Service.Abstract;

public interface IDoctorService : IGenericService<Doctor, int>
{
    Task<OperationResponse<object>> AddAsync(DoctorCreateRequest request);
    Task<OperationResponse<object>> UpdateAsync(int id, DoctorUpdateRequest request);
    Task<OperationResponse<object>> UpdateAsync(Doctor doctor);
}