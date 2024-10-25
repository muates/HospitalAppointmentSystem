using HospitalAppointmentSystem.Application.Converter;
using HospitalAppointmentSystem.Application.Service.Abstract;
using HospitalAppointmentSystem.Core.Model.Response;
using HospitalAppointmentSystem.Core.Service.Concrete;
using HospitalAppointmentSystem.DataAccess.Repository.Abstract;
using HospitalAppointmentSystem.Model.Dto.Doctor.Request;
using HospitalAppointmentSystem.Model.Entity;
using ApplicationException = HospitalAppointmentSystem.CrossCutting.Exceptions.ApplicationException;

namespace HospitalAppointmentSystem.Application.Service.Concrete;

public class DoctorService(IDoctorRepository repository)
    : GenericService<IDoctorRepository, Doctor, int>(repository), IDoctorService
{
    private readonly IDoctorRepository _repository = repository;

    public async Task<OperationResponse<object>> AddAsync(DoctorCreateRequest request)
    {
        try
        {
            var doctor = DoctorConverter.ToEntity(request);
            
            await _repository.AddAsync(doctor);
            
            return new OperationResponse<object>(201, "Doctor added successfully", null);
        }
        catch (Exception e)
        {
            throw new ApplicationException(e.Message);
        }
    }

    public async Task<OperationResponse<object>> UpdateAsync(int id, DoctorUpdateRequest request)
    {
        var existDoctor = await GetByIdAsync(id);

        AutoMapperService.Map(existDoctor, request);

        try
        {
            if (existDoctor.Data != null) await _repository.UpdateAsync(existDoctor.Data);

            return new OperationResponse<object>(200, "Doctor updated successfully", null);
        }
        catch (Exception e)
        {
            throw new ApplicationException(e.Message);
        }
    }

    public async Task<OperationResponse<object>> UpdateAsync(Doctor doctor)
    {
        try
        {
            await _repository.UpdateAsync(doctor);
            
            return new OperationResponse<object>(201, "Doctor updated successfully", null);
        }
        catch (Exception e)
        {
            throw new ApplicationException(e.Message);
        }
    }
}