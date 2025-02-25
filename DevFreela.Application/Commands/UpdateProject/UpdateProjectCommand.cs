using MediatR;

namespace DevFreela.Application.Commands.UpdateProject;

public class UpdateProjectCommand : IRequest
{
    public required int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required decimal TotalCost { get; set; }
}
