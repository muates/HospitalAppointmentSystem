using HospitalAppointmentSystem.EntityLayer.Abstract;
using HospitalAppointmentSystem.EntityLayer.Enum;

namespace HospitalAppointmentSystem.EntityLayer.Model;

public class Appointment : EntityBase<string>
{
    public int DoctorId { get; set; } = default!;
    public int PatientId { get; set; } = default!;
    public DateTime AppointmentDate { get; set; } = default!;
    public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;
    public string Notes { get; set; } = string.Empty;
}