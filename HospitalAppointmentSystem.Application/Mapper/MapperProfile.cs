using AutoMapper;
using HospitalAppointmentSystem.Model.Dto.Appointment.Request;
using HospitalAppointmentSystem.Model.Dto.Doctor.Request;
using HospitalAppointmentSystem.Model.Dto.Patient.Request;
using HospitalAppointmentSystem.Model.Entity;

namespace HospitalAppointmentSystem.Application.Mapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<DoctorUpdateRequest, Doctor>();
        CreateMap<PatientUpdateRequest, Patient>();
        CreateMap<AppointmentUpdateRequest, Appointment>();
    }
}