using MediatR;

namespace DevFreela.Application.Commands.FinishProject;

public class FinishProjectCommand(int id) : IRequest
{
    public int Id { get; init; } = id;
}
