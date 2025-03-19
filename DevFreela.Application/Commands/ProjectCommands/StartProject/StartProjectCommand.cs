using MediatR;

namespace DevFreela.Application.Commands.ProjectCommands.StartProject;

public record StartProjectCommand(int Id) : IRequest
{
}
