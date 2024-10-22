using HospitalAppointmentSystem.Core.Repository.Concrete;
using HospitalAppointmentSystem.DataAccess.Context;
using HospitalAppointmentSystem.DataAccess.Repository.Abstract;
using HospitalAppointmentSystem.Model.Entity;

namespace HospitalAppointmentSystem.DataAccess.Repository.Concrete;

public class DoctorRepository(PostgreSqlDbContext context)
    : GenericRepository<PostgreSqlDbContext, Doctor, int>(context), IDoctorRepository
{
    
}