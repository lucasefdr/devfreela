using DevFreela.Core.Entities;

namespace DevFreela.Application.Repositories;

public interface ICommentRepository
{
    Task CreateAsync(ProjectComment comment);
    Task CommitAsync();
}
