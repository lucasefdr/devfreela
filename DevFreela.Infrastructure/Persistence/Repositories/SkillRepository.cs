using DevFreela.Application.Common;
using DevFreela.Application.Repositories;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repositories;

public class SkillRepository(IRepository<Skill> repository) : ISkillRepository
{
    #region CREATE
    public async Task<Skill> CreateAsync(Skill entity)
    {
        await repository.CreateAsync(entity);
        return entity;
    }
    #endregion

    #region READ
    public async Task<Skill?> FindAsync(int id)
    {
        var skill = await repository.FindAsync(id);
        return skill;
    }

    public async Task<PagedResult<Skill>> GetAllAsync(QueryParameters parameters)
    {
        string[] searchableProperties = ["Description"];

        var query = repository.GetAll();
        query = query.ApplyQueryParameters(parameters, searchableProperties);

        return await query.ToPagedResultAsync(parameters);
    }
    #endregion

    public async Task CommitAsync()
    {
        await repository.CommitAsync();
    }
}
