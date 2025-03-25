using DevFreela.Application.Common;
using DevFreela.Core.Entities;

namespace DevFreela.Application.Repositories;

public interface ISkillRepository
{
    Task<PagedResult<Skill>> GetAllAsync(QueryParameters parameters);
    Task<Skill?> FindAsync(int id);
    Task<Skill> CreateAsync(Skill entity);
    Task CommitAsync();
}
