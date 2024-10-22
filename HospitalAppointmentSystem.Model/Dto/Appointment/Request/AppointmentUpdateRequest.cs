using System.ComponentModel.DataAnnotations;
using HospitalAppointmentSystem.Model.Enum;

namespace HospitalAppointmentSystem.Model.Dto.Appointment.Request;

public class AppointmentUpdateRequest
{
    [Required(ErrorMessage = "Status cannot be empty.")]
    public AppointmentStatus Status { get; set; }
}