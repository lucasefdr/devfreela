using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using DevFreela.Core.Common;
using DevFreela.Application.Repositories;

namespace DevFreela.Infrastructure.Persistence.Repositories;

public class Repository<TEntity>(DevFreelaDbContext context) : IRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly DevFreelaDbContext _context = context;
    protected readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    public IQueryable<TEntity> GetAll()
    {
        return _dbSet;
    }

    public async Task<TEntity?> FindAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<TEntity?> GetByAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await _dbSet.FirstOrDefaultAsync(expression);
    }

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        return entity;
    }

    public Task UpdateAsync(TEntity entity)
    {
        _dbSet.Update(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(TEntity entity)
    {
        _dbSet.Remove(entity);
        return Task.CompletedTask;
    }

    public async Task CommitAsync() => await _context.SaveChangesAsync();
}
