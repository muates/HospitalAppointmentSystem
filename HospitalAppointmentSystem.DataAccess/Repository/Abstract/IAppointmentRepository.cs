using HospitalAppointmentSystem.Core.Repository.Abstract;
using HospitalAppointmentSystem.Model.Entity;
using HospitalAppointmentSystem.Model.Enum;

namespace HospitalAppointmentSystem.DataAccess.Repository.Abstract;

public interface IAppointmentRepository : IGenericRepository<Appointment, string>
{
    Task<bool> HasAvailableAppointmentSlotsAsync(int doctorId);
    Task<IEnumerable<Appointment>> GetExpiredAppointmentsAsync(params AppointmentStatus[] statuses);
}