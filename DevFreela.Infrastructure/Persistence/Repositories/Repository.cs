using DevFreela.Core.Repositories;
using System.Linq.Expressions;
using System;
using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repositories;

public class Repository<TEntity>(DevFreelaDbContext context) : IRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly DevFreelaDbContext _context = context;


    public IQueryable<TEntity> Get()
    {
        return _context.Set<TEntity>();
    }

    public async Task<TEntity?> FindAsync(int id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }

    public async Task<TEntity?> GetByAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await _context.Set<TEntity>().FirstOrDefaultAsync(expression);
    }

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task UpdateAsync(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TEntity entity)
    {
        _context.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task CommitAsync() => await _context.SaveChangesAsync();
}
