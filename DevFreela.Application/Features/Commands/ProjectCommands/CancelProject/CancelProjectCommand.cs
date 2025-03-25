using MediatR;

namespace DevFreela.Application.Features.Commands.ProjectCommands.CancelProject;

public record CancelProjectCommand(int Id) : IRequest { }
