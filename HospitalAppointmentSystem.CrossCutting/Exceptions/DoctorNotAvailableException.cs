namespace HospitalAppointmentSystem.CrossCutting.Exceptions;

public class DoctorNotAvailableException(string message) : Exception(message);