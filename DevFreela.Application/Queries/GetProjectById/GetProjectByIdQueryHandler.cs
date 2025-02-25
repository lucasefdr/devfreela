using DevFreela.Application.ViewModels.Project;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.GetProjectById;

public class GetProjectByIdQueryHandler(DevFreelaDbContext context) : IRequestHandler<GetProjectByIdQuery, ProjectDetailsViewModel?>
{
    private readonly DevFreelaDbContext _context = context;

    public async Task<ProjectDetailsViewModel?> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var project = await _context.Projects
            .AsNoTracking()
            .Include(p => p.Client)
            .Include(p => p.Freelancer)
            .SingleOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (project is null) return null;

        return new ProjectDetailsViewModel(
            project.Id,
            project.Title,
            project.TotalCost,
            project.Description,
            project.StartedAt,
            project.FinishedAt,
            project.Client!.FullName,
            project.Freelancer!.FullName);
    }
}
