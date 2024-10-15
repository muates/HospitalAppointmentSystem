using HospitalAppointmentSystem.ApplicationLayer.Common.Response;
using HospitalAppointmentSystem.ApplicationLayer.Dto.Doctor.Request;
using HospitalAppointmentSystem.EntityLayer.Model;

namespace HospitalAppointmentSystem.ApplicationLayer.Service.Abstract;

public interface IDoctorService : IGenericService<Doctor, int>
{
    Task<ApiResponse<object>> AddAsync(DoctorCreateRequest request);
    void Update(int id, DoctorUpdateRequest request);
    void Update(Doctor doctor);
}