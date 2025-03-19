using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.ProjectCommands.FinishProject;

public class FinishProjectCommandHandler : IRequestHandler<FinishProjectCommand>
{
    private readonly IProjectRepository _projectRepository;

    public FinishProjectCommandHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task Handle(FinishProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.FindAsync(request.Id);

        project?.Finish();

        await _projectRepository.CommitAsync();
    }
}
