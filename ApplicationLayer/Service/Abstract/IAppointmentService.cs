using HospitalAppointmentSystem.ApplicationLayer.Common.Response;
using HospitalAppointmentSystem.ApplicationLayer.Dto.Appointment.Request;
using HospitalAppointmentSystem.EntityLayer.Model;

namespace HospitalAppointmentSystem.ApplicationLayer.Service.Abstract;

public interface IAppointmentService : IGenericService<Appointment, string>
{
    Task<ApiResponse<object>> AddAsync(AppointmentCreateRequest request);
    void Update(string id, AppointmentUpdateRequest request);
    Task<bool> HasAvailableAppointmentSlotsAsync(int doctorId);
    Task CancelExpiredAppointmentsAsync();
}