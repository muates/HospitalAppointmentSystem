namespace HospitalAppointmentSystem.CrossCutting.Exceptions;

public class ApplicationException(string message) : Exception(message);