using DevFreela.Application.DTOs.ViewModels.Project;
using MediatR;

namespace DevFreela.Application.Features.Queries.ProjectQueries.GetProject;

public class GetProjectQuery(int id) : IRequest<ProjectDetailsViewModel?>
{
    public int Id { get; init; } = id;
}
