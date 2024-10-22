using HospitalAppointmentSystem.Model.Dto.Patient.Request;
using HospitalAppointmentSystem.Model.Dto.Patient.Response;
using HospitalAppointmentSystem.Model.Entity;

namespace HospitalAppointmentSystem.Application.Converter;

public class PatientConverter
{
    public static Patient ToEntity(PatientCreateRequest request)
    {
        return new Patient()
        {
            Name = request.Name,
            DateOfBirth = request.DateOfBirth,
            Gender = request.Gender,
            BloodType = request.BloodType,
            MedicalHistory = request.MedicalHistory,
            ContactNumber = request.ContactNumber,
            Email = request.Email,
        };
    }

    public static PatientResponse ToDto(Patient patient)
    {
        return new PatientResponse()
        {
            Id = patient.Id,
            Name = patient.Name,
            DateOfBirth = patient.DateOfBirth,
            Gender = patient.Gender,
            BloodType = patient.BloodType,
        };
    }
}