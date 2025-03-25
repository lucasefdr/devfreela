using MediatR;

namespace DevFreela.Application.Features.Commands.ProjectCommands.StartProject;

public record StartProjectCommand(int Id) : IRequest
{
}
