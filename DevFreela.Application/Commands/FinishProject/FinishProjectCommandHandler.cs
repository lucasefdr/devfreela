using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.FinishProject;

public class FinishProjectCommandHandler(DevFreelaDbContext context) : IRequestHandler<FinishProjectCommand>
{
    private readonly DevFreelaDbContext _context = context;

    public async Task Handle(FinishProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _context.Projects.SingleOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
        project?.Finish();
        await _context.SaveChangesAsync(cancellationToken);
    }
}
