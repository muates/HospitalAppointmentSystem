using HospitalAppointmentSystem.ApplicationLayer.Service.Abstract;
using HospitalAppointmentSystem.ApplicationLayer.Service.Concrete;
using HospitalAppointmentSystem.DataAccessLayer.Repository.Abstract;
using HospitalAppointmentSystem.DataAccessLayer.Repository.Concrete;
using HospitalAppointmentSystem.EntityLayer.Model;

namespace HospitalAppointmentSystem.ApiLayer.Extension;

public static class ServiceRegistrationExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IDoctorService, DoctorService>();
        services.AddScoped<IPatientService, PatientService>();
        services.AddScoped<IAppointmentService, AppointmentService>();

        return services;
    }

    public static IServiceCollection AddDataAccessServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
        services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        services.AddScoped<IDoctorRepository, DoctorRepository>();
        services.AddScoped<IPatientRepository, PatientRepository>();
    
        return services;
    }
}