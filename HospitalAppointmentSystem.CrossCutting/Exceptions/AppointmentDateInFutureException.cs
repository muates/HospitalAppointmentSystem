namespace HospitalAppointmentSystem.CrossCutting.Exceptions;

public class AppointmentDateInFutureException : Exception
{
    public AppointmentDateInFutureException(string message) : base(message)
    {
    }

    public AppointmentDateInFutureException(string message, Exception innerException) : base(message, innerException)
    {
    }
}