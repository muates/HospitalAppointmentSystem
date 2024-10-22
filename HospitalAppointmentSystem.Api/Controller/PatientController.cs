using HospitalAppointmentSystem.Application.Converter;
using HospitalAppointmentSystem.Application.Service.Abstract;
using HospitalAppointmentSystem.Core.Model.Response;
using HospitalAppointmentSystem.Model.Dto.Patient.Request;
using HospitalAppointmentSystem.Model.Dto.Patient.Response;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAppointmentSystem.Api.Controller;

[ApiController]
[Route("api/v1/patients")]
public class PatientController(IPatientService patientService) : ControllerBase
{
    private readonly IPatientService _patientService = patientService;

    [HttpPost("create")]
    public async Task<IActionResult> CreatePatient([FromBody] PatientCreateRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(new OperationResponse<object>(400, "Invalid request data.", string.Empty));

        var result = await _patientService.AddAsync(request);

        if (result.StatusCode == StatusCodes.Status201Created)
            return Ok(new OperationResponse<object>(result.StatusCode, result.Message, result.Data));

        return BadRequest(new OperationResponse<object>(result.StatusCode, result.Message, result.Data));
    }

    [HttpGet("get/{id:int}")]
    public async Task<IActionResult> GetPatientById([FromRoute] int id)
    {
        var result = await _patientService.GetByIdAsync(id);

        if (result.Data is null)
        {
            return NotFound(new OperationResponse<object>(result.StatusCode, result.Message, result.Data));
        }

        return Ok(new OperationResponse<PatientResponse>(result.StatusCode, result.Message,
            PatientConverter.ToDto(result.Data)));
    }

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllPatient()
    {
        var result = await _patientService.GetAllAsync();

        if (result.StatusCode == 404)
        {
            return NotFound(new OperationResponse<object>(result.StatusCode, result.Message, result.Data));
        }

        return Ok(new OperationResponse<IEnumerable<PatientResponse>>(result.StatusCode, result.Message,
            result.Data?.Select(PatientConverter.ToDto)));
    }

    [HttpDelete("delete/{id:int}")]
    public async Task<IActionResult> DeletePatient([FromRoute] int id)
    {
        var result = await _patientService.DeleteAsync(id);

        if (result.StatusCode == 404)
        {
            return NotFound(new OperationResponse<object>(result.StatusCode, result.Message, result.Data));
        }

        return Ok(new OperationResponse<object>(result.StatusCode, result.Message, result.Data));
    }
}