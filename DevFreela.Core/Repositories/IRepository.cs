using DevFreela.Core.Entities;
using System.Linq.Expressions;

namespace DevFreela.Core.Repositories;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    IQueryable<TEntity> Get();
    Task<TEntity?> FindAsync(int id); // Melhor performance ao buscar por Primary Keys
    Task<TEntity?> GetByAsync(Expression<Func<TEntity, bool>> expression);
    Task<TEntity> CreateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task CommitAsync();
}
