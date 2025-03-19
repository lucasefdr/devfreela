using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories;

public interface IUserRepository
{
    IQueryable<User> Get();
    Task<User?> FindAsync(int id);
    Task<User> CreateAsync(User entity);
    Task CommitAsync();
}
