using DevFreela.Application.Common;
using DevFreela.Application.Repositories;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace DevFreela.Infrastructure.Persistence.Repositories;

public class ProjectRepository(IRepository<Project> repository) : IProjectRepository
{
    public Task<Project?> GetProjectWithComments(int id, CancellationToken cancellationToken)
    {
        return repository.GetAll()
                          .AsNoTracking()
                          .Include(p => p.Comments)
                            .ThenInclude(c => c.User)
                           .FirstOrDefaultAsync(p => p.ID == id, cancellationToken);
    }

    public async Task<IEnumerable<Project>> GetAll()
    {
        return await repository.GetAll().ToListAsync();
    }

    public async Task<PagedResult<Project>> GetPaginatedProjectsAsync(QueryParameters parameters)
    {
        string[] searchableProperties = ["Title"];

        var query = repository.GetAll();
        query = query.ApplyQueryParameters(parameters, searchableProperties);

        return await query.ToPagedResultAsync(parameters);
    }

    public async Task<Project?> GetWithDetailsAsync(int id)
    {
        var project = await repository.GetAll()
                                        .Include(p => p.Client)
                                        .Include(p => p.Freelancer)
                                        .FirstOrDefaultAsync(p => p.ID == id);

        return project;
    }

    public async Task<Project?> FindAsync(int id)
    {
        return await repository.FindAsync(id);
    }

    public async Task<Project> CreateAsync(Project entity)
    {
        await repository.CreateAsync(entity);
        return entity;
    }

    public async Task CommitAsync()
    {
        await repository.CommitAsync();
    }
}
