using DevFreela.Application.DTOs.ViewModels.Project;
using MediatR;

namespace DevFreela.Application.Features.Queries.ProjectQueries.GetAllProjects;

public class GetAllProjectsQuery : IRequest<IEnumerable<ProjectViewModel>>
{
}
