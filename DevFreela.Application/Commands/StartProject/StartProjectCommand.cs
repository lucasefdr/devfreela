using MediatR;

namespace DevFreela.Application.Commands.StartProject;

public class StartProjectCommand(int id) : IRequest
{
    public int Id { get; init; } = id;
}
