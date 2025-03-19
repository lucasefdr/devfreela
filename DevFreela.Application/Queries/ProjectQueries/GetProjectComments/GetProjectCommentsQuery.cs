using DevFreela.Application.ViewModels.Project;
using MediatR;

namespace DevFreela.Application.Queries.ProjectQueries.GetProjectComments;

public class GetProjectCommentsQuery(int projectId) : IRequest<ProjectCommentsViewModel?>
{
    public int ProjectId { get; set; } = projectId;
}
