using System.Diagnostics;
using HospitalAppointmentSystem.CrossCutting.Logger.Concrete;

namespace HospitalAppointmentSystem.Api.Middleware;

public class LoggingMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        var requestPath = context.Request.Path;
        var requestMethod = context.Request.Method;
        var userId = context.User?.Identity?.IsAuthenticated == true ? context.User.FindFirst("id")?.Value : "Anonymous";

        GlobalLogger.LogInfo($"[{userId}] Entering method {requestMethod} {requestPath}");

        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            GlobalLogger.LogError($"[{userId}] Error in method {requestMethod} {requestPath}: {ex.Message}");
            throw;
        }
        finally
        {
            stopwatch.Stop();
            GlobalLogger.LogInfo($"[{userId}] Exiting method {requestMethod} {requestPath} in {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}