namespace HospitalAppointmentSystem.CrossCutting.Exceptions;

public class DoctorNotAvailableException : Exception
{
    public DoctorNotAvailableException(string message) : base(message)
    {
    }

    public DoctorNotAvailableException(string message, Exception innerException) : base(message, innerException)
    {
    }
}