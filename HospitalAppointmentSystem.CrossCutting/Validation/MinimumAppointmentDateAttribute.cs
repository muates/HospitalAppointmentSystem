using System.ComponentModel.DataAnnotations;

namespace HospitalAppointmentSystem.CrossCutting.Validation;

public class MinimumAppointmentDateAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var appointmentDate = (DateTime)(value ?? throw new ArgumentNullException(nameof(value)));
        var minAppointmentDate = DateTime.Now.AddDays(3);

        if (appointmentDate < minAppointmentDate)
        {
            return new ValidationResult("Appointment date must be at least 3 days from today!!!");
        }
        
        return ValidationResult.Success;
    }
}