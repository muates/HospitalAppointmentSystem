using HospitalAppointmentSystem.CrossCutting.Logger.Abstract;
using Microsoft.Extensions.Logging;

namespace HospitalAppointmentSystem.CrossCutting.Logger.Concrete;

public class LoggerService(ILogger<LoggerService> logger) : ILoggerService
{
    private readonly ILogger<LoggerService> _logger = logger;

    public void LogInfo(string message)
    {
        _logger.LogInformation(message);
    }

    public void LogError(string message)
    {
        _logger.LogError(message);
    }

    public void LogWarning(string message)
    {
        _logger.LogWarning(message);
    }
}