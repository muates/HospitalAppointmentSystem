using HospitalAppointmentSystem.Model.Dto.Appointment.Request;
using HospitalAppointmentSystem.Model.Dto.Appointment.Response;
using HospitalAppointmentSystem.Model.Entity;

namespace HospitalAppointmentSystem.Application.Converter;

public class AppointmentConverter
{
    public static Appointment ToEntity(AppointmentCreateRequest request)
    {
        return new Appointment()
        {
            DoctorId = request.DoctorId,
            PatientId = request.PatientId,
            AppointmentDate = request.AppointmentDate,
            Notes = request.Notes
        };
    }
    
    public static AppointmentResponse ToDto(Appointment appointment)
    {
        return new AppointmentResponse()
        {
            Id = appointment.Id,
            PatientId = appointment.PatientId,
            DoctorId = appointment.DoctorId,
            AppointmentDate = appointment.AppointmentDate,
            Status = appointment.Status,
        };
    }
}