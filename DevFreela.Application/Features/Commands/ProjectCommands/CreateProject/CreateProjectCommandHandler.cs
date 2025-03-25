using DevFreela.Application.Repositories;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Features.Commands.ProjectCommands.CreateProject;

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, int>
{
    private readonly IProjectRepository _projectRepository;

    public CreateProjectCommandHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<int> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = new Project(request.Title, request.Description, request.IdClient, request.IdFreelancer, request.TotalCost);

        await _projectRepository.CreateAsync(project);
        await _projectRepository.CommitAsync();

        return project.ID;
    }
}
