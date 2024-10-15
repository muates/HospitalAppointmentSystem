using System.Linq.Expressions;
using HospitalAppointmentSystem.ApplicationLayer.Common.Response;
using HospitalAppointmentSystem.ApplicationLayer.Converter;
using HospitalAppointmentSystem.ApplicationLayer.Dto.Doctor.Request;
using HospitalAppointmentSystem.ApplicationLayer.Service.Abstract;
using HospitalAppointmentSystem.DataAccessLayer.Repository.Abstract;
using HospitalAppointmentSystem.EntityLayer.Model;

namespace HospitalAppointmentSystem.ApplicationLayer.Service.Concrete;

public class DoctorService(IDoctorRepository doctorRepository) : IDoctorService
{
    private readonly IDoctorRepository _doctorRepository = doctorRepository;

    public async Task<ApiResponse<IEnumerable<Doctor>>> GetAllAsync()
    {
        var doctors = await _doctorRepository.GetAllAsync();

        var enumerable = doctors as Doctor[] ?? doctors.ToArray();

        return enumerable.Length == 0
            ? new ApiResponse<IEnumerable<Doctor>>(404, "No doctors found.", null)
            : new ApiResponse<IEnumerable<Doctor>>(200, "Doctors retrieved successfully.", enumerable);
    }

    public async Task<ApiResponse<Doctor>> GetByIdAsync(int id)
    {
        var doctor = await _doctorRepository.GetByIdAsync(id);

        return doctor is null
            ? new ApiResponse<Doctor>(404, "Doctor not found", null)
            : new ApiResponse<Doctor>(200, "Success", doctor);
    }

    public async Task<ApiResponse<IEnumerable<Doctor>>> FindAsync(Expression<Func<Doctor, bool>>? predicate = null)
    {
        var doctors = await _doctorRepository.FindAsync(predicate);
        return new ApiResponse<IEnumerable<Doctor>>(200, "Success", doctors);
    }

    public async Task<ApiResponse<object>> DeleteAsync(int id)
    {
        await _doctorRepository.DeleteAsync(id);
        return new ApiResponse<object>(200, "Doctor deleted successfully", null);
    }

    public async Task<ApiResponse<object>> AddAsync(DoctorCreateRequest request)
    {
        var doctor = DoctorConverter.ToEntity(request);
        await _doctorRepository.AddAsync(doctor);
        return new ApiResponse<object>(201, "Doctor added successfully", null);
    }

    public void Update(int id, DoctorUpdateRequest request)
    {
        throw new NotImplementedException();
    }

    public void Update(Doctor doctor)
    {
        _doctorRepository.Update(doctor);
    }
}