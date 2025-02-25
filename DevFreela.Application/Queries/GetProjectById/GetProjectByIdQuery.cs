using DevFreela.Application.ViewModels.Project;
using MediatR;

namespace DevFreela.Application.Queries.GetProjectById;

public class GetProjectByIdQuery(int id) : IRequest<ProjectDetailsViewModel?>
{
    public int Id { get; init; } = id;
}
