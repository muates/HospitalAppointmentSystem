using HospitalAppointmentSystem.Model.Enum;

namespace HospitalAppointmentSystem.Model.Dto.Appointment.Response;

public class AppointmentResponse
{
    public string Id { get; set; } = string.Empty;
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public AppointmentStatus Status { get; set; }
}