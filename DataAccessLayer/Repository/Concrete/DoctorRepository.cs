using HospitalAppointmentSystem.DataAccessLayer.Context;
using HospitalAppointmentSystem.DataAccessLayer.Repository.Abstract;
using HospitalAppointmentSystem.EntityLayer.Model;

namespace HospitalAppointmentSystem.DataAccessLayer.Repository.Concrete;

public class DoctorRepository : GenericRepository<Doctor, int>, IDoctorRepository
{
    public DoctorRepository(PostgreSqlDbContext context) : base(context) { }
}