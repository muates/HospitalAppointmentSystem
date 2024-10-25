using System.Diagnostics;
using HospitalAppointmentSystem.CrossCutting.Logger.Concrete;

namespace HospitalAppointmentSystem.Api.Middleware;

public class LoggingMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        GlobalLogger.LogInfo($"Entering method {context.Request.Method} {context.Request.Path}");

        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            GlobalLogger.LogError($"Error in method {context.Request.Method} {context.Request.Path}.", ex);
        }
        finally
        {
            stopwatch.Stop();
            GlobalLogger.LogInfo($"Exiting method {context.Request.Method} {context.Request.Path} in {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}