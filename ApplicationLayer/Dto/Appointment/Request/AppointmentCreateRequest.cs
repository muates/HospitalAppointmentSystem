using System.ComponentModel.DataAnnotations;
using HospitalAppointmentSystem.ApplicationLayer.Validation;

namespace HospitalAppointmentSystem.ApplicationLayer.Dto.Appointment.Request;

public class AppointmentCreateRequest
{
    [Required(ErrorMessage = "Doctor ID cannot be empty.")]
    public int DoctorId { get; set; }

    [Required(ErrorMessage = "Patient ID cannot be empty.")]
    public int PatientId { get; set; }
    
    [MinimumAppointmentDate]
    public DateTime AppointmentDate { get; set; }
    
    public string Notes { get; set; } = string.Empty;
}