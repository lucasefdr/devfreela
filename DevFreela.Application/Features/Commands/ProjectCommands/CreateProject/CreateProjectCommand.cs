using MediatR;

namespace DevFreela.Application.Features.Commands.ProjectCommands.CreateProject;

public record CreateProjectCommand(
    string Title,
    string Description,
    int ClientId,
    int FreelancerId,
    decimal TotalCost) : IRequest<int>
{ }

