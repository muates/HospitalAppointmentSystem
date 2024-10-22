using HospitalAppointmentSystem.Core.Model.Entity;
using HospitalAppointmentSystem.Model.Enum;

namespace HospitalAppointmentSystem.Model.Entity;

public sealed class Doctor : EntityBase<int>
{
    public string Name { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public Branch Branch { get; set; }
    public int YearsOfExperience { get; set; }
    public string Hospital { get; set; } = string.Empty;
    public List<Patient> Patients { get; private set; } = [];
}