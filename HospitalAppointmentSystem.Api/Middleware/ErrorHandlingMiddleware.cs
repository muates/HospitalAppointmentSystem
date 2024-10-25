using HospitalAppointmentSystem.Core.Model.Response;
using HospitalAppointmentSystem.CrossCutting.Exceptions;
using ApplicationException = System.ApplicationException;

namespace HospitalAppointmentSystem.Api.Middleware;

public class ErrorHandlingMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var errorMessage = $"Error occurred: {ex.Message}";

        var response = ex switch
        {
            AppointmentDateInFutureException => new OperationResponse<object>(400, errorMessage, null),
            DoctorNotAvailableException => new OperationResponse<object>(400, errorMessage, null),
            ValidationException => new OperationResponse<object>(403, errorMessage, null),
            NotFoundException => new OperationResponse<object>(404, errorMessage, null),
            ApplicationException => new OperationResponse<object>(500, errorMessage, null),
            _ => new OperationResponse<object>(500, errorMessage, null)
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = response.StatusCode;

        return context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
    }
}