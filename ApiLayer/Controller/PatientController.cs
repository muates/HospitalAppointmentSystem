using HospitalAppointmentSystem.ApplicationLayer.Common.Response;
using HospitalAppointmentSystem.ApplicationLayer.Converter;
using HospitalAppointmentSystem.ApplicationLayer.Dto.Patient.Request;
using HospitalAppointmentSystem.ApplicationLayer.Dto.Patient.Response;
using HospitalAppointmentSystem.ApplicationLayer.Service.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAppointmentSystem.ApiLayer.Controller;

[ApiController]
[Route("api/v1/patients")]
public class PatientController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePatient([FromBody] PatientCreateRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<string>(400, "Invalid request data.", string.Empty));

        var result = await _patientService.AddAsync(request);

        if (result.StatusCode == StatusCodes.Status201Created)
            return Ok(new ApiResponse<string>(StatusCodes.Status201Created, "Patient added successfully",
                string.Empty));

        return BadRequest(new ApiResponse<string>(result.StatusCode, result.Message, string.Empty));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetPatientById(int id)
    {
        var result = await _patientService.GetByIdAsync(id);

        if (result.Data is null)
        {
            return NotFound(new ApiResponse<string>(404, "Patient not found.", string.Empty));
        }

        return Ok(new ApiResponse<PatientResponse>(200, "Patient retrieved successfully.",
            PatientConverter.ToDto(result.Data)));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPatients()
    {
        var result = await _patientService.GetAllAsync();

        if (result.StatusCode == 404)
        {
            return NotFound(new ApiResponse<IEnumerable<PatientResponse>>(404, "No patients found.",
                Enumerable.Empty<PatientResponse>()));
        }

        var response = result.Data.Select(PatientConverter.ToDto);
        return Ok(new ApiResponse<IEnumerable<PatientResponse>>(200, "Patients retrieved successfully.", response));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePatient(int id)
    {
        var result = await _patientService.DeleteAsync(id);

        if (result.StatusCode == 404)
        {
            return NotFound(new ApiResponse<object>(404, "Patient not found.", null));
        }

        return Ok(new ApiResponse<object>(200, "Patient deleted successfully.", null));
    }
}