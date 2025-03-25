using DevFreela.Application.Common;
using DevFreela.Core.Entities;

namespace DevFreela.Application.Repositories;

public interface IUserRepository
{
    Task<PagedResult<User>> GetFreelancersAsync(QueryParameters parameters);
    Task<PagedResult<User>> GetClientsAsync(QueryParameters parameters);


    IQueryable<User> Get();
    Task<User?> FindAsync(int id);
    Task<User?> GetUserWithSkillsAsync(int id);
    Task<User> CreateAsync(User entity);
    Task CommitAsync();
}
