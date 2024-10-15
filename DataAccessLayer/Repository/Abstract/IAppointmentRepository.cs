using HospitalAppointmentSystem.EntityLayer.Model;

namespace HospitalAppointmentSystem.DataAccessLayer.Repository.Abstract;

public interface IAppointmentRepository : IGenericRepository<Appointment, string>
{
    Task<bool> HasAvailableAppointmentSlotsAsync(int doctorId);
    Task<IEnumerable<Appointment>> GetExpiredAppointmentsAsync();
}