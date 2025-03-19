using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repositories;

public class SkillRepository(IRepository<Skill> repository) : ISkillRepository
{
    private readonly IRepository<Skill> _repository = repository;

    public async Task<Skill> CreateAsync(Skill entity)
    {
        await _repository.CreateAsync(entity);
        return entity;
    }

    public async Task<Skill?> FindAsync(int id)
    {
        var skill = await _repository.FindAsync(id);
        return skill;
    }

    public IQueryable<Skill> Get()
    {
        return _repository.Get().AsNoTracking();
    }
}
