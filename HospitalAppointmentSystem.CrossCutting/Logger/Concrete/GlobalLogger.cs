using HospitalAppointmentSystem.CrossCutting.Logger.Abstract;

namespace HospitalAppointmentSystem.CrossCutting.Logger.Concrete;

public static class GlobalLogger
{
    private static ILoggerService? _loggingService;

    public static void Configure(ILoggerService? loggingService)
    {
        _loggingService = loggingService;
    }

    public static void LogInfo(string message)
    {
        _loggingService?.LogInfo(message);
    }

    public static void LogError(string message, Exception ex)
    {
        _loggingService?.LogError(message, ex);
    }

    public static void LogWarning(string message)
    {
        _loggingService?.LogWarning(message);
    }
}
