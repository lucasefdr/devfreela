using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.ProjectCommands.StartProject;

public class StartProjectCommandHandler : IRequestHandler<StartProjectCommand>
{
    private readonly IProjectRepository _projectRepository;

    public StartProjectCommandHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task Handle(StartProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.FindAsync(request.Id);

        project?.Start();

        await _projectRepository.CommitAsync();
    }
}
