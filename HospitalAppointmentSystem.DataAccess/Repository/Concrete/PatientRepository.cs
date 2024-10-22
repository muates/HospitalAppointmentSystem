using HospitalAppointmentSystem.Core.Repository.Concrete;
using HospitalAppointmentSystem.DataAccess.Context;
using HospitalAppointmentSystem.DataAccess.Repository.Abstract;
using HospitalAppointmentSystem.Model.Entity;

namespace HospitalAppointmentSystem.DataAccess.Repository.Concrete;

public class PatientRepository(PostgreSqlDbContext context)
    : GenericRepository<PostgreSqlDbContext, Patient, int>(context), IPatientRepository
{

}