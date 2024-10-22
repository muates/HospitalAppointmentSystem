using HospitalAppointmentSystem.Application.Converter;
using HospitalAppointmentSystem.Application.Service.Abstract;
using HospitalAppointmentSystem.Core.Model.Response;
using HospitalAppointmentSystem.Model.Dto.Appointment.Request;
using HospitalAppointmentSystem.Model.Dto.Appointment.Response;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAppointmentSystem.Api.Controller;

[ApiController]
[Route("api/v1/appointments")]
public class AppointmentController(IAppointmentService appointmentService) : ControllerBase
{
    private readonly IAppointmentService _appointmentService = appointmentService;

    [HttpPost("create")]
    public async Task<IActionResult> CreateAppointment([FromBody] AppointmentCreateRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(new OperationResponse<object>(400, "Invalid request data.", string.Empty));

        var result = await _appointmentService.AddAsync(request);

        if (result.StatusCode == StatusCodes.Status201Created)
            return Ok(new OperationResponse<object>(result.StatusCode, result.Message, result.Data));

        return BadRequest(new OperationResponse<object>(result.StatusCode, result.Message, result.Data));
    }

    [HttpGet("get/{id}")]
    public async Task<IActionResult> GetAppointmentById([FromRoute] string id)
    {
        var result = await _appointmentService.GetByIdAsync(id);

        if (result.Data is null)
        {
            return NotFound(new OperationResponse<object>(result.StatusCode, result.Message, result.Data));
        }

        return Ok(new OperationResponse<AppointmentResponse>(result.StatusCode, result.Message,
            AppointmentConverter.ToDto(result.Data)));
    }

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAppointments()
    {
        var result = await _appointmentService.GetAllAsync();

        if (result.StatusCode == 404)
        {
            return NotFound(new OperationResponse<object>(result.StatusCode, result.Message, result.Data));
        }

        return Ok(new OperationResponse<IEnumerable<AppointmentResponse>>(result.StatusCode, result.Message,
            result.Data?.Select(AppointmentConverter.ToDto)));
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteAppointment([FromRoute] string id)
    {
        var result = await _appointmentService.DeleteAsync(id);

        if (result.StatusCode == 404)
        {
            return NotFound(new OperationResponse<object>(result.StatusCode, result.Message, result.Data));
        }

        return Ok(new OperationResponse<object>(result.StatusCode, result.Message, result.Data));
    }
}