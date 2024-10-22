namespace HospitalAppointmentSystem.Core.Model.Response;

public class OperationResponse<T>(int statusCode, string message, T? data)
{
    public int StatusCode { get; set; } = statusCode;
    public string Message { get; set; } = message;
    public T? Data { get; set; } = data;
}