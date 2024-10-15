namespace HospitalAppointmentSystem.ApplicationLayer.Common.Response;

public class ApiResponse<T>
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }

    public ApiResponse(int statusCode, string message, T? data)
    {
        StatusCode = statusCode;
        Message = message;
        Data = data;
    }
}