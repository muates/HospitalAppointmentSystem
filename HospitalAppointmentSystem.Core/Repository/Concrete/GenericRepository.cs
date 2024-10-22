using System.Linq.Expressions;
using HospitalAppointmentSystem.Core.Model.Entity;
using HospitalAppointmentSystem.Core.Repository.Abstract;
using Microsoft.EntityFrameworkCore;

namespace HospitalAppointmentSystem.Core.Repository.Concrete;

public abstract class GenericRepository<TContext, TEntity, TId>(TContext context) : IGenericRepository<TEntity, TId>
    where TEntity : EntityBase<TId>, new()
    where TContext : DbContext
{
    protected TContext Context { get; } = context;

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await Context.Set<TEntity>().ToListAsync();
    }

    public async Task<TEntity?> GetByIdAsync(TId id)
    {
        return await Context.Set<TEntity>().FindAsync(id);
    }

    public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>>? predicate = null)
    {
        return predicate is null
            ? await GetAllAsync()
            : await Context.Set<TEntity>().Where(predicate).ToListAsync();
    }

    public async Task AddAsync(TEntity entity)
    {
        await Context.Set<TEntity>().AddAsync(entity);
        await Context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TEntity entity)
    { 
        Context.Set<TEntity>().Update(entity);
        await Context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TId id)
    {
        var entity = await GetByIdAsync(id);
        if (entity is not null)
        {
            Context.Set<TEntity>().Remove(entity);
            await Context.SaveChangesAsync();
        }
    }
}