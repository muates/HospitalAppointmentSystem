using HospitalAppointmentSystem.DataAccessLayer.Context;
using HospitalAppointmentSystem.DataAccessLayer.Repository.Abstract;
using HospitalAppointmentSystem.EntityLayer.Enum;
using HospitalAppointmentSystem.EntityLayer.Model;
using Microsoft.EntityFrameworkCore;

namespace HospitalAppointmentSystem.DataAccessLayer.Repository.Concrete;

public class AppointmentRepository : GenericRepository<Appointment, string>, IAppointmentRepository
{
    public AppointmentRepository(PostgreSqlDbContext context) : base(context)
    {
    }

    public async Task<bool> HasAvailableAppointmentSlotsAsync(int doctorId)
    {
        var appointmentCount = await _context.Set<Appointment>()
            .CountAsync(a =>
                a.DoctorId == doctorId &&
                a.AppointmentDate > DateTime.UtcNow &&  
                a.Status == AppointmentStatus.Pending);

        return appointmentCount < 10;
    }

    public async Task<IEnumerable<Appointment>> GetExpiredAppointmentsAsync()
    {
        var currentDate = DateTime.UtcNow;
        return await _context.Set<Appointment>()
            .Where(a => a.AppointmentDate < currentDate && a.Status != AppointmentStatus.Canceled)
            .ToListAsync();
    }
}