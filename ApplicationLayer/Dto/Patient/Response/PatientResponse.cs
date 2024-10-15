using HospitalAppointmentSystem.EntityLayer.Enum;

namespace HospitalAppointmentSystem.ApplicationLayer.Dto.Patient.Response;

public class PatientResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public string BloodType { get; set; } = string.Empty;
}