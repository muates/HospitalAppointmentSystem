using System.Linq.Expressions;
using HospitalAppointmentSystem.Core.Model.Entity;
using HospitalAppointmentSystem.Core.Model.Response;

namespace HospitalAppointmentSystem.Core.Service.Abstract;

public interface IGenericService<TEntity, in TId> where TEntity : EntityBase<TId>, new()
{
    Task<OperationResponse<TEntity>> GetByIdAsync(TId id);
    Task<OperationResponse<IEnumerable<TEntity>>> GetAllAsync();
    Task<OperationResponse<IEnumerable<TEntity>>> FindAsync(Expression<Func<TEntity, bool>>? predicate = null);
    Task<OperationResponse<object>> DeleteAsync(TId id);
}