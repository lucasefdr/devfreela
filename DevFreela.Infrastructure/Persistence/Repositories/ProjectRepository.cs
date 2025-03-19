using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repositories;

public class ProjectRepository(IRepository<Project> repository) : IProjectRepository
{
    private readonly IRepository<Project> _repository = repository;

    public IQueryable<Project> Get()
    {
        return _repository.Get().AsNoTracking();
    }

    public async Task<Project?> GetWithDetailsAsync(int id)
    {
        var project = await _repository.Get()
                                        .Include(p => p.Client)
                                        .Include(p => p.Freelancer)
                                        .FirstOrDefaultAsync(p => p.Id == id);

        return project;
    }

    public async Task<Project?> FindAsync(int id)
    {
        return await _repository.FindAsync(id);
    }

    public async Task<Project> CreateAsync(Project entity)
    {
        await _repository.CreateAsync(entity);
        return entity;
    }

    public async Task CommitAsync()
    {
        await _repository.CommitAsync();
    }
}
