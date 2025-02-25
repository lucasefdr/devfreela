using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.UpdateProject;

public class UpdateProjectCommandHandler(DevFreelaDbContext context) : IRequestHandler<UpdateProjectCommand>
{
    private readonly DevFreelaDbContext _context = context;

    public async Task Handle(UpdateProjectCommand command, CancellationToken cancellationToken)
    {
        var project = await _context.Projects.SingleOrDefaultAsync(p => p.Id == command.Id, cancellationToken);
        project?.Update(command.Title, command.Description, command.TotalCost);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
