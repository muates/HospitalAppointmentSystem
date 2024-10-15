using System.Linq.Expressions;
using HospitalAppointmentSystem.DataAccessLayer.Repository.Abstract;
using HospitalAppointmentSystem.EntityLayer.Abstract;
using Microsoft.EntityFrameworkCore;

namespace HospitalAppointmentSystem.DataAccessLayer.Repository.Concrete;

public class GenericRepository<TEntity, TId> : IGenericRepository<TEntity, TId> where TEntity : EntityBase<TId>
{
    protected readonly DbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    protected GenericRepository(DbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<TEntity?> GetByIdAsync(TId id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>>? predicate  = null)
    {
        return predicate == null 
            ? await _dbSet.ToListAsync()
            : await _dbSet.Where(predicate).ToListAsync();
    }

    public async Task AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public void Update(TEntity entity)
    {
        _dbSet.Update(entity);
        _context.SaveChanges();
    }

    public async Task DeleteAsync(TId id)
    {
        var entity = await GetByIdAsync(id);
        if (entity is not null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}