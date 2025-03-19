using DevFreela.Application.ViewModels.Project;
using MediatR;

namespace DevFreela.Application.Queries.ProjectQueries.GetAllProjects;

public class GetAllProjectsQuery : IRequest<IEnumerable<ProjectViewModel>>
{
}
