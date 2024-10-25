namespace HospitalAppointmentSystem.CrossCutting.Logger.Abstract;

public interface ILoggerService
{
    void LogInfo(string message);
    void LogError(string message, Exception ex);
    void LogWarning(string message);
}