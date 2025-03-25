using DevFreela.Core.Common;
using System.Linq.Expressions;

namespace DevFreela.Application.Repositories;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    IQueryable<TEntity> GetAll();
    Task<TEntity?> FindAsync(int id); // Melhor performance ao buscar por Primary Keys
    Task<TEntity?> GetByAsync(Expression<Func<TEntity, bool>> expression);
    Task<TEntity> CreateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task CommitAsync();
}
