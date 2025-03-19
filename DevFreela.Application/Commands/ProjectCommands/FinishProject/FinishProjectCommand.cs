using MediatR;

namespace DevFreela.Application.Commands.ProjectCommands.FinishProject;

public record FinishProjectCommand(int Id) : IRequest { }
