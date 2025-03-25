using DevFreela.Application.Common;
using DevFreela.Application.DTOs.InputModels.Project;
using DevFreela.Application.DTOs.ViewModels.Project;
using DevFreela.Application.Repositories;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using System.Reflection;

namespace DevFreela.Application.Services.Implementations;

public class ProjectService(IProjectRepository projectRepository,
                            IUserRepository userRepository,
                            ICommentRepository commentRepository) : IProjectService
{
    #region CREATE
    public async Task<int> Create(CreateProjectInputModel model)
    {
        var client = await userRepository.FindAsync(model.ClientID) ?? throw new KeyNotFoundException("Client not found");

        if (client.UserType != UserTypeEnum.CLIENT)
            throw new ArgumentException("User is not a client");

        if (!client.IsActive)
            throw new ArgumentException("Client is not active");

        var freelancer = await userRepository.FindAsync(model.FreelancerID) ?? throw new KeyNotFoundException("Freelancer not found");

        if (freelancer.UserType != UserTypeEnum.FREELANCER)
            throw new ArgumentException("User is not a freelancer");

        if (!client.IsActive)
            throw new ArgumentException("Freelancer is not active");

        var newProject = new Project(model.Title, model.Description, model.ClientID, model.FreelancerID, model.TotalCost);
        await projectRepository.CreateAsync(newProject);
        await projectRepository.CommitAsync();

        return newProject.ID;
    }

    public async Task CreateComment(CreateCommentInputModel model)
    {
        var project = await projectRepository.FindAsync(model.ProjectID) ?? throw new KeyNotFoundException("Project not found");
        var user = await userRepository.FindAsync(model.UserID) ?? throw new KeyNotFoundException("Project not found");

        var comment = new ProjectComment(model.Content, model.ProjectID, model.UserID);

        await commentRepository.CreateAsync(comment);
        await commentRepository.CommitAsync();
    }
    #endregion

    #region UPDATE
    public async Task Cancel(int projectID)
    {
        var project = await FindProject(projectID) ?? throw new KeyNotFoundException("Project not found");

        project.Cancel();
        await projectRepository.CommitAsync();
    }

    public async Task Start(int projectID)
    {
        var project = await FindProject(projectID) ?? throw new KeyNotFoundException("Project not found");

        project.Start();
        await projectRepository.CommitAsync();
    }

    public async Task Update(int projectID, UpdateProjectInputModel model)
    {
        var project = await FindProject(projectID) ?? throw new KeyNotFoundException("Project not found");

        project.Update(model.Title, model.Description, model.TotalCost);
        await projectRepository.CommitAsync();
    }

    public async Task Finish(int projectID)
    {
        var project = await FindProject(projectID) ?? throw new KeyNotFoundException("Project not found");

        project.Finish();
        await projectRepository.CommitAsync();
    }
    #endregion

    #region READ
    public async Task<PagedResult<ProjectViewModel>> GetAll(QueryParameters parameters)
    {
        var projects = await projectRepository.GetPaginatedProjectsAsync(parameters);

        var projectsResponse = new PagedResult<ProjectViewModel>
        {
            CurrentPage = projects.CurrentPage,
            PageSize = projects.PageSize,
            TotalCount = projects.TotalCount,
            TotalPages = projects.TotalPages,
            Items = [.. projects.Items.Select(p => new ProjectViewModel(p.ID, p.Title, p.CreatedAt))],
        };

        return projectsResponse;
    }

    public async Task<ProjectDetailsViewModel?> GetByID(int id)
    {
        var project = await projectRepository.GetWithDetailsAsync(id) ?? throw new KeyNotFoundException("Project not found");

        return new ProjectDetailsViewModel(project.ID, project.Title, project.TotalCost, project.Description,
                                           project.StartedAt, project.FinishedAt, project.CancelledAt, project.Status, project.Client!.FullName,
                                           project.Freelancer!.FullName);
    }
    #endregion

    #region Métodos
    private async Task<Project?> FindProject(int id)
    {
        return await projectRepository.FindAsync(id);
    }
    #endregion

}