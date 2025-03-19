using DevFreela.Application.ViewModels.Project;
using MediatR;

namespace DevFreela.Application.Queries.ProjectQueries.GetProject;

public class GetProjectQuery(int id) : IRequest<ProjectDetailsViewModel?>
{
    public int Id { get; init; } = id;
}
