using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories;

public interface ISkillRepository
{
    IQueryable<Skill> Get();
    Task<Skill?> FindAsync(int id);
    Task<Skill> CreateAsync(Skill entity);
}
