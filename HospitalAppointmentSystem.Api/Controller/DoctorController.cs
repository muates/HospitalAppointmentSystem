using HospitalAppointmentSystem.Application.Converter;
using HospitalAppointmentSystem.Application.Service.Abstract;
using HospitalAppointmentSystem.Core.Model.Response;
using HospitalAppointmentSystem.Model.Dto.Doctor.Request;
using HospitalAppointmentSystem.Model.Dto.Doctor.Response;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAppointmentSystem.Api.Controller;

[ApiController]
[Route("api/v1/doctors")]
public class DoctorController(IDoctorService doctorService) : ControllerBase
{
    private readonly IDoctorService _doctorService = doctorService;

    [HttpPost("create")]
    public async Task<IActionResult> CreateDoctor([FromBody] DoctorCreateRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(new OperationResponse<object>(400, "Invalid request data.", string.Empty));

        var result = await _doctorService.AddAsync(request);

        if (result.StatusCode == StatusCodes.Status201Created)
            return Ok(new OperationResponse<object>(result.StatusCode, result.Message, result.Data));

        return BadRequest(new OperationResponse<object>(result.StatusCode, result.Message, result.Data));
    }

    [HttpGet("get/{id:int}")]
    public async Task<IActionResult> GetDoctorById([FromRoute] int id)
    {
        var result = await _doctorService.GetByIdAsync(id);

        if (result.Data is null)
        {
            return NotFound(new OperationResponse<object>(result.StatusCode, result.Message, result.Data));
        }

        return Ok(new OperationResponse<DoctorResponse>(result.StatusCode, result.Message,
            DoctorConverter.ToDto(result.Data)));
    }

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllDoctors()
    {
        var result = await _doctorService.GetAllAsync();
        
        if (result.StatusCode == 404)
        {
            return NotFound(new OperationResponse<object>(result.StatusCode, result.Message, result.Data));
        }

        return Ok(new OperationResponse<IEnumerable<DoctorResponse>>(result.StatusCode, result.Message,
            result.Data?.Select(DoctorConverter.ToDto)));
    }

    [HttpDelete("delete/{id:int}")]
    public async Task<IActionResult> DeleteDoctor([FromRoute] int id)
    {
        var result = await _doctorService.DeleteAsync(id);
        
        if (result.StatusCode == 404)
        {
            return NotFound(new OperationResponse<object>(result.StatusCode, result.Message, result.Data));
        }

        return Ok(new OperationResponse<object>(result.StatusCode, result.Message, result.Data));
    }
}