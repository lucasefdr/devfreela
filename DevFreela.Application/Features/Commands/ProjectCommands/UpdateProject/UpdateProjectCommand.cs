using MediatR;

namespace DevFreela.Application.Features.Commands.ProjectCommands.UpdateProject;

public record UpdateProjectCommand(int Id, string Title, string Description, decimal TotalCost) : IRequest
{

}
