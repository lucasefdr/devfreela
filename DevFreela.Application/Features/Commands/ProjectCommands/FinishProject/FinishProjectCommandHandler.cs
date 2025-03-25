using DevFreela.Application.Repositories;
using MediatR;

namespace DevFreela.Application.Features.Commands.ProjectCommands.FinishProject;

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
