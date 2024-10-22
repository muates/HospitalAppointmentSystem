using System.Linq.Expressions;
using HospitalAppointmentSystem.Core.Model.Entity;

namespace HospitalAppointmentSystem.Core.Repository.Abstract;

public interface IGenericRepository<TEntity, in TId> where TEntity : EntityBase<TId>, new()
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(TId id);
    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>>? predicate = null);
    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TId id);
}