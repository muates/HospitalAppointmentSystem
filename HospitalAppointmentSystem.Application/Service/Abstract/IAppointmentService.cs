using HospitalAppointmentSystem.Core.Model.Response;
using HospitalAppointmentSystem.Core.Service.Abstract;
using HospitalAppointmentSystem.Model.Dto.Appointment.Request;
using HospitalAppointmentSystem.Model.Entity;

namespace HospitalAppointmentSystem.Application.Service.Abstract;

public interface IAppointmentService : IGenericService<Appointment, string>
{
    Task<OperationResponse<object>> AddAsync(AppointmentCreateRequest request);
    Task<OperationResponse<object>> UpdateAsync(string id, AppointmentUpdateRequest request);
    Task<bool> HasAvailableAppointmentSlotsAsync(int doctorId);
    Task CancelExpiredAppointmentsAsync();
}