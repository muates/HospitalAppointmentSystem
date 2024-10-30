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

    public static void LogError(string message)
    {
        _loggingService?.LogError(message);
    }

    public static void LogWarning(string message)
    {
        _loggingService?.LogWarning(message);
    }
}
