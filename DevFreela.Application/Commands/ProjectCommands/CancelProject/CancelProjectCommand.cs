using MediatR;

namespace DevFreela.Application.Commands.ProjectCommands.CancelProject;

public record CancelProjectCommand(int Id) : IRequest { }
