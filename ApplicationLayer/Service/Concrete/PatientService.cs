using System.Linq.Expressions;
using HospitalAppointmentSystem.ApplicationLayer.Common.Response;
using HospitalAppointmentSystem.ApplicationLayer.Converter;
using HospitalAppointmentSystem.ApplicationLayer.Dto.Patient.Request;
using HospitalAppointmentSystem.ApplicationLayer.Service.Abstract;
using HospitalAppointmentSystem.DataAccessLayer.Repository.Abstract;
using HospitalAppointmentSystem.EntityLayer.Model;

namespace HospitalAppointmentSystem.ApplicationLayer.Service.Concrete;

public class PatientService(IPatientRepository patientRepository) : IPatientService
{
    private readonly IPatientRepository _patientRepository = patientRepository;

    public async Task<ApiResponse<IEnumerable<Patient>>> GetAllAsync()
    {
        var patients = await _patientRepository.GetAllAsync();

        var enumerable = patients as Patient[] ?? patients.ToArray();

        return enumerable.Length == 0
            ? new ApiResponse<IEnumerable<Patient>>(404, "No patients found.", null)
            : new ApiResponse<IEnumerable<Patient>>(200, "Patients retrieved successfully.", enumerable);
    }

    public async Task<ApiResponse<Patient>> GetByIdAsync(int id)
    {
        var patient = await _patientRepository.GetByIdAsync(id);

        return patient is null
            ? new ApiResponse<Patient>(404, "Patient not found", null)
            : new ApiResponse<Patient>(200, "Success", patient);
    }

    public async Task<ApiResponse<IEnumerable<Patient>>> FindAsync(Expression<Func<Patient, bool>>? predicate = null)
    {
        var patients = await _patientRepository.FindAsync(predicate);
        return new ApiResponse<IEnumerable<Patient>>(200, "Success", patients);
    }

    public async Task<ApiResponse<object>> DeleteAsync(int id)
    {
        await _patientRepository.DeleteAsync(id);
        return new ApiResponse<object>(200, "Patient deleted successfully", null);
    }

    public async Task<ApiResponse<object>> AddAsync(PatientCreateRequest request)
    {
        var patient = PatientConverter.Convert(request);
        await _patientRepository.AddAsync(patient);
        return new ApiResponse<object>(201, "Patient added successfully", null);
    }

    public void Update(int id, PatientUpdateRequest request)
    {
        throw new NotImplementedException();
    }
}