using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.CancelProject;

public class CancelProjectCommandHandler : IRequestHandler<CancelProjectCommand>
{
    private readonly DevFreelaDbContext _context;

    public CancelProjectCommandHandler(DevFreelaDbContext context)
    {
        _context = context;
    }

    public async Task Handle(CancelProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _context.Projects.SingleOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
        project?.Cancel();
        await _context.SaveChangesAsync(cancellationToken);
    }
}
