using MediatR;

namespace DevFreela.Application.Features.Commands.ProjectCommands.CreateProject;

public record CreateProjectCommand(
    string Title,
    string Description,
    int IdClient,
    int IdFreelancer,
    decimal TotalCost) : IRequest<int>
{ }

