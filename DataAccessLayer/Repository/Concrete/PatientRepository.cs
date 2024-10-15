using HospitalAppointmentSystem.DataAccessLayer.Context;
using HospitalAppointmentSystem.DataAccessLayer.Repository.Abstract;
using HospitalAppointmentSystem.EntityLayer.Model;

namespace HospitalAppointmentSystem.DataAccessLayer.Repository.Concrete;

public class PatientRepository : GenericRepository<Patient, int>, IPatientRepository
{
    public PatientRepository(PostgreSqlDbContext context) : base(context) { }
}