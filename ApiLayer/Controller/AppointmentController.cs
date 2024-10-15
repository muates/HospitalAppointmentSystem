using HospitalAppointmentSystem.ApplicationLayer.Common.Response;
using HospitalAppointmentSystem.ApplicationLayer.Converter;
using HospitalAppointmentSystem.ApplicationLayer.Dto.Appointment.Request;
using HospitalAppointmentSystem.ApplicationLayer.Dto.Appointment.Response;
using HospitalAppointmentSystem.ApplicationLayer.Service.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAppointmentSystem.ApiLayer.Controller;

[ApiController]
[Route("api/v1/appointments")]
public class AppointmentController(IAppointmentService appointmentService) : ControllerBase
{
    private readonly IAppointmentService _appointmentService = appointmentService;

    [HttpPost]
    public async Task<IActionResult> CreateAppointment([FromBody] AppointmentCreateRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<string>(400, "Invalid request data.", string.Empty));

        var result = await _appointmentService.AddAsync(request);

        if (result.StatusCode == StatusCodes.Status201Created)
            return Ok(new ApiResponse<string>(StatusCodes.Status201Created, "Appointment added successfully",
                string.Empty));

        return BadRequest(new ApiResponse<string>(result.StatusCode, result.Message, string.Empty));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAppointmentById(string id)
    {
        var appointment = await _appointmentService.GetByIdAsync(id);

        if (appointment.Data is null)
        {
            return NotFound(new ApiResponse<string>(404, "Appointment not found.", string.Empty));
        }

        return Ok(new ApiResponse<AppointmentResponse>(200, "Appointment retrieved successfully.",
            AppointmentConverter.ToDto(appointment.Data)));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAppointments()
    {
        var result = await _appointmentService.GetAllAsync();

        if (result.StatusCode == 404)
        {
            return NotFound(new ApiResponse<IEnumerable<AppointmentResponse>>(404, "No appointments found.",
                Enumerable.Empty<AppointmentResponse>()));
        }

        var response = result.Data.Select(AppointmentConverter.ToDto);
        return Ok(new ApiResponse<IEnumerable<AppointmentResponse>>(200, "Appointments retrieved successfully.",
            response));
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAppointment(string id)
    {
        var result = await _appointmentService.DeleteAsync(id);

        if (result.StatusCode == 404)
        {
            return NotFound(new ApiResponse<object>(404, "Appointment not found.", null));
        }

        return Ok(new ApiResponse<object>(200, "Appointment deleted successfully.", null));
    }
}