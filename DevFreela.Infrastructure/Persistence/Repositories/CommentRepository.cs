using DevFreela.Application.Repositories;
using DevFreela.Core.Entities;

namespace DevFreela.Infrastructure.Persistence.Repositories;

public class CommentRepository(IRepository<ProjectComment> repository) : ICommentRepository
{
    public async Task CreateAsync(ProjectComment comment)
    {
        await repository.CreateAsync(comment);
    }

    public async Task CommitAsync() => await repository.CommitAsync();
}
