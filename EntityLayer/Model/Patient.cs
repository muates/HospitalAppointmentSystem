using HospitalAppointmentSystem.EntityLayer.Abstract;
using HospitalAppointmentSystem.EntityLayer.Enum;

namespace HospitalAppointmentSystem.EntityLayer.Model;

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