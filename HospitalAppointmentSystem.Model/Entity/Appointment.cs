using HospitalAppointmentSystem.Core.Model.Entity;
using HospitalAppointmentSystem.Model.Enum;

namespace HospitalAppointmentSystem.Model.Entity;

public sealed class Appointment : EntityBase<string>
{
    public int DoctorId { get; set; }
    public int PatientId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;
    public string Notes { get; set; } = string.Empty;
}