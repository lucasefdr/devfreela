using DevFreela.Application.Common;
using DevFreela.Core.Common;
using DevFreela.Core.Entities;

namespace DevFreela.Application.Repositories;

public interface IUserRepository
{
    Task<PagedResult<User>> GetFreelancersAsync(QueryParameters parameters);
    Task<User?> GetFreelancerWithDetailsAsync(int id);
    Task<PagedResult<User>> GetClientsAsync(QueryParameters parameters);
    Task<User?> GetClientWithDetailsAsync(int id);



    Task<User?>  Get(int id);
    Task<User?> FindAsync(int id);


    Task CommitAsync();
}
