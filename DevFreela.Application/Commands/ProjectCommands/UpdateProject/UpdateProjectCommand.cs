using MediatR;

namespace DevFreela.Application.Commands.ProjectCommands.UpdateProject;

public record UpdateProjectCommand(int Id, string Title, string Description, decimal TotalCost) : IRequest
{

}
