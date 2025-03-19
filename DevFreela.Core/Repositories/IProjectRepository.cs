using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories;

public interface IProjectRepository
{
    IQueryable<Project> Get();
    Task<Project?> GetWithDetailsAsync(int id);
    Task<Project?> FindAsync(int id);
    Task<Project> CreateAsync(Project entity);
    Task CommitAsync();
}
