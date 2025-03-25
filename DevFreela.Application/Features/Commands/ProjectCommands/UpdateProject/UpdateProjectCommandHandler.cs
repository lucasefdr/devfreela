using DevFreela.Application.Repositories;
using MediatR;

namespace DevFreela.Application.Features.Commands.ProjectCommands.UpdateProject;

public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand>
{
    private readonly IProjectRepository _projectRepository;

    public UpdateProjectCommandHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task Handle(UpdateProjectCommand command, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.FindAsync(command.Id);

        project?.Update(command.Title, command.Description, command.TotalCost);

        await _projectRepository.CommitAsync();
    }
}
