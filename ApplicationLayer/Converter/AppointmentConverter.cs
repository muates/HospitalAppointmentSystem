using HospitalAppointmentSystem.ApplicationLayer.Dto.Appointment.Request;
using HospitalAppointmentSystem.ApplicationLayer.Dto.Appointment.Response;
using HospitalAppointmentSystem.EntityLayer.Model;

namespace HospitalAppointmentSystem.ApplicationLayer.Converter;

public class AppointmentConverter
{
    public static Appointment ToEntity(AppointmentCreateRequest request)
    {
        return new Appointment()
        {
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