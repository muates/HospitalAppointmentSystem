using System.ComponentModel.DataAnnotations;
using HospitalAppointmentSystem.Model.Enum;

namespace HospitalAppointmentSystem.Model.Dto.Patient.Request;

public class PatientCreateRequest
{
    [Required(ErrorMessage = "The name field cannot be empty.")]
    public string Name { get; set; } = string.Empty;
    
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public string BloodType { get; set; } = string.Empty;
    public string MedicalHistory { get; set; } = string.Empty;
    public string ContactNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}