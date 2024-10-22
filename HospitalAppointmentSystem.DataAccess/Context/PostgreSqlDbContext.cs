using HospitalAppointmentSystem.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HospitalAppointmentSystem.DataAccess.Context;

public class PostgreSqlDbContext(DbContextOptions<PostgreSqlDbContext> options) : DbContext(options)
{
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var dateTimeProperties = entityType.ClrType.GetProperties()
                .Where(p => p.PropertyType == typeof(DateTime) || p.PropertyType == typeof(DateTime?));

            foreach (var property in dateTimeProperties)
            {
                var converter = new ValueConverter<DateTime, DateTime>(
                    v => v.ToUniversalTime(),
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
                );

                modelBuilder.Entity(entityType.Name)
                    .Property(property.Name)
                    .HasConversion(converter);
            }
        }
    }
}