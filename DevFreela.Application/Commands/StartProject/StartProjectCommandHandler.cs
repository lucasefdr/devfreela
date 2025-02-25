using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.StartProject;

public class StartProjectCommandHandler : IRequestHandler<StartProjectCommand>
{
    private readonly DevFreelaDbContext _context;

    public StartProjectCommandHandler(DevFreelaDbContext context)
    {
        _context = context;
    }

    public async Task Handle(StartProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _context.Projects.SingleOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
        project?.Start();
        await _context.SaveChangesAsync(cancellationToken);
    }
}
