using System.Linq.Expressions;
using HospitalAppointmentSystem.Core.Model.Entity;
using HospitalAppointmentSystem.Core.Model.Response;
using HospitalAppointmentSystem.Core.Repository.Abstract;
using HospitalAppointmentSystem.Core.Service.Abstract;

namespace HospitalAppointmentSystem.Core.Service.Concrete;

public abstract class GenericService<TRepository, TEntity, TId>(TRepository repository) : IGenericService<TEntity, TId>
    where TEntity : EntityBase<TId>, new()
    where TRepository : IGenericRepository<TEntity, TId>
{
    private readonly TRepository _repository = repository;

    public async Task<OperationResponse<TEntity>> GetByIdAsync(TId id)
    {
        var result = await _repository.GetByIdAsync(id);

        return result is null
            ? new OperationResponse<TEntity>(404, $"{typeof(TEntity).Name} Not Found", null)
            : new OperationResponse<TEntity>(200, "Success", result);
    }

    public async Task<OperationResponse<IEnumerable<TEntity>>> GetAllAsync()
    {
        var result = await _repository.GetAllAsync();

        var enumerable = result as TEntity[] ?? result.ToArray();

        return enumerable.Length == 0
            ? new OperationResponse<IEnumerable<TEntity>>(404, $"{typeof(TEntity).Name}s Not Found", null)
            : new OperationResponse<IEnumerable<TEntity>>(200, $"{typeof(TEntity).Name} Retrieved Successfully",
                enumerable);
    }

    public async Task<OperationResponse<IEnumerable<TEntity>>> FindAsync(
        Expression<Func<TEntity, bool>>? predicate = null)
    {
        var result = await _repository.FindAsync(predicate);

        var enumerable = result as TEntity[] ?? result.ToArray();

        return enumerable.Length == 0
            ? new OperationResponse<IEnumerable<TEntity>>(404, $"{typeof(TEntity).Name} Not Found", null)
            : new OperationResponse<IEnumerable<TEntity>>(200, $"{typeof(TEntity).Name} Retrieved Successfully",
                enumerable);
    }

    public async Task<OperationResponse<object>> DeleteAsync(TId id)
    {
        try
        {
            await _repository.DeleteAsync(id);
            return new OperationResponse<object>(200, "Success", null);
        }
        catch (Exception ex)
        {
            return new OperationResponse<object>(500, $"Error deleting {typeof(TEntity).Name}: {ex.Message}", null);
        }
    }
}