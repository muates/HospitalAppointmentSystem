using HospitalAppointmentSystem.Core.Model.Entity;
using HospitalAppointmentSystem.Model.Enum;

namespace HospitalAppointmentSystem.Model.Entity;

public sealed class Patient : EntityBase<int>
{
    public string Name { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; } = DateTime.Now;
    public Gender Gender { get; set; }
    public string BloodType { get; set; } = string.Empty;
    public string MedicalHistory { get; set; } = string.Empty;
    public string ContactNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}