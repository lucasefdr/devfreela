using MediatR;

namespace DevFreela.Application.Commands.CancelProject;

public class CancelProjectCommand(int id) : IRequest
{
    public int Id { get; init; } = id;
}
