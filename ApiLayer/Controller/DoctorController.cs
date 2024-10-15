using HospitalAppointmentSystem.ApplicationLayer.Common.Response;
using HospitalAppointmentSystem.ApplicationLayer.Converter;
using HospitalAppointmentSystem.ApplicationLayer.Dto.Doctor.Request;
using HospitalAppointmentSystem.ApplicationLayer.Dto.Doctor.Response;
using HospitalAppointmentSystem.ApplicationLayer.Service.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAppointmentSystem.ApiLayer.Controller;

[ApiController]
[Route("api/v1/doctors")]
public class DoctorController : ControllerBase
{
    private readonly IDoctorService _doctorService;

    public DoctorController(IDoctorService doctorService)
    {
        _doctorService = doctorService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateDoctor([FromBody] DoctorCreateRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<string>(400, "Invalid request data.", string.Empty));

        var result = await _doctorService.AddAsync(request);

        if (result.StatusCode == StatusCodes.Status201Created)
            return Ok(new ApiResponse<string>(StatusCodes.Status201Created, "Doctor added successfully", string.Empty));

        return BadRequest(new ApiResponse<string>(result.StatusCode, result.Message, string.Empty));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDoctorById(int id)
    {
        var result = await _doctorService.GetByIdAsync(id);

        if (result.Data is null)
        {
            return NotFound(new ApiResponse<string>(404, "Doctor not found.", string.Empty));
        }

        return Ok(new ApiResponse<DoctorResponse>(200, "Doctor retrieved successfully.",
            DoctorConverter.ToDto(result.Data)));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllDoctors()
    {
        var result = await _doctorService.GetAllAsync();

        if (result.StatusCode == 404)
        {
            return NotFound(new ApiResponse<IEnumerable<DoctorResponse>>(404, "No doctors found.",
                Enumerable.Empty<DoctorResponse>()));
        }

        var response = result.Data.Select(DoctorConverter.ToDto);
        return Ok(new ApiResponse<IEnumerable<DoctorResponse>>(200, "Doctors retrieved successfully.", response));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDoctor(int id)
    {
        var result = await _doctorService.DeleteAsync(id);

        if (result.StatusCode == 404)
        {
            return NotFound(new ApiResponse<object>(404, "Doctor not found.", null));
        }

        return Ok(new ApiResponse<object>(200, "Doctor deleted successfully.", null));
    }
}