using HospitalAppointmentSystem.Core.Repository.Concrete;
using HospitalAppointmentSystem.DataAccess.Context;
using HospitalAppointmentSystem.DataAccess.Repository.Abstract;
using HospitalAppointmentSystem.Model.Entity;
using HospitalAppointmentSystem.Model.Enum;
using Microsoft.EntityFrameworkCore;

namespace HospitalAppointmentSystem.DataAccess.Repository.Concrete;

public class AppointmentRepository(PostgreSqlDbContext context)
    : GenericRepository<PostgreSqlDbContext, Appointment, string>(context), IAppointmentRepository
{
    private readonly PostgreSqlDbContext _context = context;

    public async Task<bool> HasAvailableAppointmentSlotsAsync(int doctorId)
    {
        var appointmentCount = await _context.Appointments
            .CountAsync(a =>
                a.DoctorId == doctorId &&
                a.AppointmentDate > DateTime.UtcNow &&  
                a.Status == AppointmentStatus.Pending);

        return appointmentCount < 10;
    }

    public async Task<IEnumerable<Appointment>> GetExpiredAppointmentsAsync(params AppointmentStatus[] statuses)
    {
        var currentDate = DateTime.UtcNow;

        return await _context.Appointments
            .Where(a => a.AppointmentDate < currentDate && statuses.Contains(a.Status))
            .ToListAsync();
    }
}