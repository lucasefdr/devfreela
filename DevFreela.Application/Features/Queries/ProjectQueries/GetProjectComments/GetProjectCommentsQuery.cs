using DevFreela.Application.DTOs.ViewModels.Project;
using MediatR;

namespace DevFreela.Application.Features.Queries.ProjectQueries.GetProjectComments;

public class GetProjectCommentsQuery(int projectId) : IRequest<ProjectCommentsViewModel?>
{
    public int ProjectId { get; set; } = projectId;
}
