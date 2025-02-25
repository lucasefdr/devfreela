using DevFreela.Application.ViewModels.Project;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.GetAllProjects;

public class GetAllProjectsQueryHandler(DevFreelaDbContext context) : IRequestHandler<GetAllProjectsQuery, List<ProjectViewModel>>
{
    private readonly DevFreelaDbContext _context = context;

    public async Task<List<ProjectViewModel>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
    {
        var projects = await _context.Projects
            .Select(p => new ProjectViewModel(p.Id, p.Title, p.CreatedAt))
            .ToListAsync(cancellationToken);

        return projects;
    }
}
