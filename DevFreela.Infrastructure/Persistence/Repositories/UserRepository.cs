using DevFreela.Application.Common;
using DevFreela.Application.Repositories;
using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using DevFreela.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repositories;

public class UserRepository(IRepository<User> repository) : IUserRepository
{
    #region READ

    public async Task<PagedResult<User>> GetFreelancersAsync(QueryParameters parameters)
    {
        string[] searchableProperties = ["FullName"];

        var query = repository.GetAll()
            .Where(u => u.UserType == UserTypeEnum.FREELANCER);

        query = query.ApplyQueryParameters(parameters, searchableProperties);
        query = query.Include(u => u.UserSkills).ThenInclude(s => s.Skill);

        return await query.ToPagedResultAsync(parameters);
    }

    public async Task<PagedResult<User>> GetClientsAsync(QueryParameters parameters)
    {
        string[] searchableProperties = ["FullName"];

        var query = repository.GetAll()
            .Where(u => u.UserType == UserTypeEnum.CLIENT);

        query = query.ApplyQueryParameters(parameters, searchableProperties);
        query = query.Include(u => u.UserSkills).ThenInclude(s => s.Skill);

        return await query.ToPagedResultAsync(parameters);
    }


    public async Task<User?> Get(int id)
    {
        return await repository.GetAll()
            .Include(u => u.UserSkills)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> GetUserWithSkillsAsync(int id)
    {
        return await repository.GetAll()
            .Include(u => u.UserSkills)
            .ThenInclude(s => s.Skill)
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> FindAsync(int id)
    {
        return await repository.FindAsync(id);
    }

    #endregion

    #region CREATE

    public async Task<User> CreateAsync(User entity)
    {
        await repository.CreateAsync(entity);
        return entity;
    }

    #endregion


    public async Task CommitAsync()
    {
        await repository.CommitAsync();
    }
}