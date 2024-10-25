namespace HospitalAppointmentSystem.CrossCutting.Exceptions;

public class ValidationException(string message) : Exception(message);