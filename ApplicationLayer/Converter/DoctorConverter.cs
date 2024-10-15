using HospitalAppointmentSystem.ApplicationLayer.Dto.Doctor.Request;
using HospitalAppointmentSystem.ApplicationLayer.Dto.Doctor.Response;
using HospitalAppointmentSystem.EntityLayer.Model;

namespace HospitalAppointmentSystem.ApplicationLayer.Converter;

public class DoctorConverter
{
    public static Doctor ToEntity(DoctorCreateRequest request)
    {
        return new Doctor()
        {
            Name = request.Name,
            Title = request.Title,
            Branch = request.Branch,
            YearsOfExperience = request.YearsOfExperience,
            Hospital = request.Hospital
        };
    }

    public static DoctorResponse ToDto(Doctor doctor)
    {
        return new DoctorResponse()
        {
            Id = doctor.Id,
            Name = doctor.Name,
            Title = doctor.Title,
        };
    }
}