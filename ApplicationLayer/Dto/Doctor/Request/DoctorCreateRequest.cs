using System.ComponentModel.DataAnnotations;
using HospitalAppointmentSystem.EntityLayer.Enum;

namespace HospitalAppointmentSystem.ApplicationLayer.Dto.Doctor.Request;

public class DoctorCreateRequest
{
    [Required(ErrorMessage = "The name field cannot be empty.")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "The title field cannot be empty.")]
    public string Title { get; set; }
    
    public Branch Branch { get; set; }
    public int YearsOfExperience { get; set; }
    public string Hospital { get; set; }
}