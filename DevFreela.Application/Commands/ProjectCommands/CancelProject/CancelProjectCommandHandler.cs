using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.ProjectCommands.CancelProject;

public class CancelProjectCommandHandler : IRequestHandler<CancelProjectCommand>
{
    private readonly IProjectRepository _projectRepository;

    public CancelProjectCommandHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task Handle(CancelProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.FindAsync(request.Id);

        project?.Cancel();

        await _projectRepository.CommitAsync();
    }
}
