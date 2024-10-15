using HospitalAppointmentSystem.EntityLayer.Abstract;
using HospitalAppointmentSystem.EntityLayer.Enum;

namespace HospitalAppointmentSystem.EntityLayer.Model;

public sealed class Doctor : EntityBase<int>
{
    public string Name { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public Branch Branch { get; set; }
    public int YearsOfExperience { get; set; }
    public string Hospital { get; set; } = string.Empty;
    public List<Patient> Patients { get; private set; } = [];
}