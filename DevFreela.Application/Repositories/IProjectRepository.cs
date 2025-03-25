using DevFreela.Application.Common;
using DevFreela.Core.Entities;

namespace DevFreela.Application.Repositories;

public interface IProjectRepository
{
    Task<Project?> GetProjectWithComments(int id, CancellationToken cancellationToken);
    Task<IEnumerable<Project>> GetAll();
    Task<PagedResult<Project>> GetPaginatedProjectsAsync(QueryParameters parameters);
    Task<Project?> GetWithDetailsAsync(int id);
    Task<Project?> FindAsync(int id);
    Task<Project> CreateAsync(Project entity);
    Task CommitAsync();
}
