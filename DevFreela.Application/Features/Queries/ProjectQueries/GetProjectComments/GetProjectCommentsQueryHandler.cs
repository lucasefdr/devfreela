using DevFreela.Application.DTOs.ViewModels.Project;
using DevFreela.Application.Repositories;
using MediatR;

namespace DevFreela.Application.Features.Queries.ProjectQueries.GetProjectComments;

public class GetProjectCommentsQueryHandler(IProjectRepository projectRepository) : IRequestHandler<GetProjectCommentsQuery, ProjectCommentsViewModel?>
{

    public async Task<ProjectCommentsViewModel?> Handle(GetProjectCommentsQuery request, CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetProjectWithComments(request.ProjectId, cancellationToken);

        if (project == null) return null;

        var commentsViewModel = project.Comments
            .Select(c => new ProjectCommentViewModel(c.Content, c.User.FullName, c.CreatedAt))
            .ToList()
            .AsReadOnly();

        return new ProjectCommentsViewModel(project.ID, project.Title, commentsViewModel);
    }
}
