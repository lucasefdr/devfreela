using MediatR;

namespace DevFreela.Application.Commands.CreateProject;

public class CreateProjectCommand : IRequest<int>
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required int IdClient { get; set; }
    public required int IdFreelancer { get; set; }
    public required decimal TotalCost { get; set; }
}
