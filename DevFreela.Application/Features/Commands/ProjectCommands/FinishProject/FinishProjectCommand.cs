using MediatR;

namespace DevFreela.Application.Features.Commands.ProjectCommands.FinishProject;

public record FinishProjectCommand(int Id) : IRequest { }
