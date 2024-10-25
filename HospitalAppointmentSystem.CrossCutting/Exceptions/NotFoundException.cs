namespace HospitalAppointmentSystem.CrossCutting.Exceptions;

public class NotFoundException(string message) : Exception(message);