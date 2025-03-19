using DevFreela.Application.InputModels.Project;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels.Project;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Services.Implementations;

public class ProjectService : IProjectService
{
    private readonly DevFreelaDbContext _context;

    public ProjectService(DevFreelaDbContext context)
    {
        _context = context;
    }

    public int Create(CreateProjectInputModel inputModel)
    {
        var project = new Project(inputModel.Title, inputModel.Description, inputModel.IdClient, inputModel.IdFreelancer, inputModel.TotalCost);
        _context.Projects.Add(project); // Insert 
        _context.SaveChanges(); // Commit

        return project.Id;
    }

    public void CreateComment(CreateCommentInputModel inputModel)
    {
        var comment = new ProjectComment(inputModel.Content, inputModel.IdProject, inputModel.IdUser);
        _context.Comments.Add(comment); // Insert
        _context.SaveChanges(); // Commit
    }

    public void Delete(int id)
    {
        var project = _context.Projects.SingleOrDefault(p => p.Id == id);
        project?.Cancel(); // Update (altera o estado)
        _context.SaveChanges(); // Commit
    }

    public void Finish(int id)
    {
        var project = _context.Projects.SingleOrDefault(p => p.Id == id);
        project?.Finish(); // Update (altera o estado)
        _context.SaveChanges(); // Commit
    }

    public List<ProjectViewModel> GetAll(string query)
    {
        var projects = _context.Projects;

        var projectsViewModel = projects
            .Select(p => new ProjectViewModel(p.Id, p.Title, p.CreatedAt))
            .ToList();

        return projectsViewModel;
    }

    public ProjectDetailsViewModel? GetById(int id)
    {
        var project = _context.Projects
            .Include(p => p.Client)
            .Include(p => p.Freelancer)
            .SingleOrDefault(p => p.Id == id);

        if (project is null) return null;

        var projectDetailsViewModel = new ProjectDetailsViewModel(
            project.Id,
            project.Title,
            project.TotalCost,
            project.Description,
            project.StartedAt,
            project.FinishedAt,
            project.Status.ToString(),
            project.Client!.FullName,
            project.Freelancer!.FullName);

        return projectDetailsViewModel;
    }

    public void Start(int id)
    {
        var project = _context.Projects.SingleOrDefault(p => p.Id == id);
        project?.Start(); // Update (altera o estado)
        _context.SaveChanges(); // Commit
    }

    public void Update(UpdateProjectInputModel inputModel)
    {
        var project = _context.Projects.SingleOrDefault(p => p.Id == inputModel.Id);

        project?.Update(inputModel.Title, inputModel.Description, inputModel.TotalCost); // Update (altera o estado)
        _context.SaveChanges();
    }
}
