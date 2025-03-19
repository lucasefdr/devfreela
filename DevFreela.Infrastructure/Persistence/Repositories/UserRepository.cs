using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;

namespace DevFreela.Infrastructure.Persistence.Repositories;

public class UserRepository(IRepository<User> repository) : IUserRepository
{
    private readonly IRepository<User> _repository = repository;

    public IQueryable<User> Get()
    {
        return _repository.Get();
    }

    public async Task<User> CreateAsync(User entity)
    {
        await _repository.CreateAsync(entity);
        return entity;
    }

    public async Task<User?> FindAsync(int id)
    {
        return await _repository.FindAsync(id);
    }

    public async Task CommitAsync()
    {
        await _repository.CommitAsync();
    }
}
