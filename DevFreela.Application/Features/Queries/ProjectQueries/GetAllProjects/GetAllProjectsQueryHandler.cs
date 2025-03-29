using DevFreela.Application.DTOs.ViewModels.Project;
using DevFreela.Application.Repositories;
using MediatR;

namespace DevFreela.Application.Features.Queries.ProjectQueries.GetAllProjects;

public class GetAllProjectsQueryHandler(IProjectRepository projectRepository) 
    : IRequestHandler<GetAllProjectsQuery, IEnumerable<ProjectViewModel>>
{
    public async Task<IEnumerable<ProjectViewModel>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
    {
        var projects = await projectRepository.GetAll();

        var projectsViewModel = projects.Select(p => new ProjectViewModel(p.Id, p.Title, p.CreatedAt));

        return projectsViewModel;
    }
}
