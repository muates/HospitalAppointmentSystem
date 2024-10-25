namespace HospitalAppointmentSystem.CrossCutting.Exceptions;

public class AppointmentDateInFutureException(string message) : Exception(message);