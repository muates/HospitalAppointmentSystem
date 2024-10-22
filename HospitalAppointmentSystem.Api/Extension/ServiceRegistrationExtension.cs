using HospitalAppointmentSystem.Application.Service.Abstract;
using HospitalAppointmentSystem.Application.Service.Concrete;
using HospitalAppointmentSystem.DataAccess.Config;
using HospitalAppointmentSystem.DataAccess.Context;
using HospitalAppointmentSystem.DataAccess.Repository.Abstract;
using HospitalAppointmentSystem.DataAccess.Repository.Concrete;
using Microsoft.EntityFrameworkCore;

namespace HospitalAppointmentSystem.Api.Extension;

public static class ServiceRegistrationExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ServiceRegistrationExtension));
        services.AddScoped<IDoctorService, DoctorService>();
        services.AddScoped<IPatientService, PatientService>();
        services.AddScoped<IAppointmentService, AppointmentService>();

        return services;
    }

    public static IServiceCollection AddDataAccessServices(this IServiceCollection services)
    {
        services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        services.AddScoped<IDoctorRepository, DoctorRepository>();
        services.AddScoped<IPatientRepository, PatientRepository>();

        return services;
    }

    public static IServiceCollection AddDatabaseServices(this IServiceCollection services)
    {
        EnvironmentConfig.LoadEnv();
        
        services.AddDbContext<PostgreSqlDbContext>(
            options => options.UseNpgsql(EnvironmentConfig.PostgreSqlConnection), ServiceLifetime.Transient);

        return services;
    }
}