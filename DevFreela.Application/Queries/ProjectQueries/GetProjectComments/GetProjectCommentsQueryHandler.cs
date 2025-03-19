using DevFreela.Application.ViewModels.Project;
using DevFreela.Core.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.ProjectQueries.GetProjectComments;

public class GetProjectCommentsQueryHandler(IProjectRepository projectRepository) : IRequestHandler<GetProjectCommentsQuery, ProjectCommentsViewModel?>
{
    private readonly IProjectRepository _projectRepository = projectRepository;

    public async Task<ProjectCommentsViewModel?> Handle(GetProjectCommentsQuery request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.Get()
                .AsNoTracking()
                .Include(p => p.Comments)
                    .ThenInclude(c => c.User)
                .FirstOrDefaultAsync(p => p.Id == request.ProjectId, cancellationToken);

        if (project == null) return null;

        var commentsViewModel = project.Comments
            .Select(c => new ProjectCommentViewModel(c.Content, c.User.FullName, c.CreatedAt))
            .ToList()
            .AsReadOnly();

        return new ProjectCommentsViewModel(project.Id, project.Title, commentsViewModel);
    }
}
