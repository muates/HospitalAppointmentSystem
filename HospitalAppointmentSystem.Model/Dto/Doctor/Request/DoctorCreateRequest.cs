using System.ComponentModel.DataAnnotations;
using HospitalAppointmentSystem.Model.Enum;

namespace HospitalAppointmentSystem.Model.Dto.Doctor.Request;

public class DoctorCreateRequest
{
    [Required(ErrorMessage = "The name field cannot be empty.")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "The title field cannot be empty.")]
    public string Title { get; set; }
    
    public Branch Branch { get; set; }
    public int YearsOfExperience { get; set; }
    public string Hospital { get; set; } = string.Empty;
}