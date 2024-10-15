using System.Linq.Expressions;
using HospitalAppointmentSystem.EntityLayer.Abstract;

namespace HospitalAppointmentSystem.DataAccessLayer.Repository.Abstract;

public interface IGenericRepository<TEntity, in TId> where TEntity : EntityBase<TId>  
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(TId id);
    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>>? predicate = null);
    Task AddAsync(TEntity entity);
    void Update(TEntity entity);
    Task DeleteAsync(TId id);
}