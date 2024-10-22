using HospitalAppointmentSystem.Core.Repository.Abstract;
using HospitalAppointmentSystem.Model.Entity;

namespace HospitalAppointmentSystem.DataAccess.Repository.Abstract;

public interface IPatientRepository : IGenericRepository<Patient, int>
{
    
}