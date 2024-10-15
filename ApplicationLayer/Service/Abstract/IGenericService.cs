using System.Linq.Expressions;
using HospitalAppointmentSystem.ApplicationLayer.Common.Response;
using HospitalAppointmentSystem.EntityLayer.Abstract;

namespace HospitalAppointmentSystem.ApplicationLayer.Service.Abstract;

public interface IGenericService<TEntity, TId> where TEntity : EntityBase<TId>
{
    Task<ApiResponse<IEnumerable<TEntity>>> GetAllAsync();
    Task<ApiResponse<TEntity>> GetByIdAsync(TId id);
    Task<ApiResponse<IEnumerable<TEntity>>> FindAsync(Expression<Func<TEntity, bool>>? predicate = null);
    Task<ApiResponse<object>> DeleteAsync(TId id);
}