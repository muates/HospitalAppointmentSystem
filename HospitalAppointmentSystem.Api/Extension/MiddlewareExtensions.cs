using HospitalAppointmentSystem.Api.Middleware;

namespace HospitalAppointmentSystem.Api.Extension;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseApplicationMiddleware(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<ErrorHandlingMiddleware>();
        builder.UseMiddleware<LoggingMiddleware>();

        return builder;
    }
}