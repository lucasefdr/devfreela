using DevFreela.Application.Common;
using DevFreela.Application.DTOs.InputModels.Project;
using DevFreela.Application.DTOs.ViewModels.Project;
using DevFreela.Application.Repositories;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using System.Reflection;
using DevFreela.Core.Common;

namespace DevFreela.Application.Services.Implementations;

public class ProjectService(
    IProjectRepository projectRepository,
    IUserRepository userRepository,
    ICommentRepository commentRepository) : IProjectService
{
    #region CREATE

    public async Task<Result<int>> Create(CreateProjectInputModel model)
    {
        var client = await userRepository.FindAsync(model.ClientId);
        if (client == null)
            return Result<int>.Failure("Client not found", 404);

        if (client.UserType != UserTypeEnum.CLIENT)
            return Result<int>.Failure("User is not CLIENT");

        if (!client.IsActive)
            return Result<int>.Failure("Client is not active");

        var freelancer = await userRepository.FindAsync(model.FreelancerId);
        if (freelancer == null)
            return Result<int>.Failure("Freelancer not found", 404);

        if (freelancer.UserType != UserTypeEnum.FREELANCER)
            return Result<int>.Failure("User is not FREELANCER");

        if (!client.IsActive)
            return Result<int>.Failure("Freelancer is not active");

        var newProject = new Project(model.Title, model.Description, model.ClientId, model.FreelancerId,
            model.TotalCost);
        
        await projectRepository.CreateAsync(newProject);
        await projectRepository.CommitAsync();

        return Result.Success(newProject.Id);
    }

    public async Task<Result>  CreateComment(CreateCommentInputModel model)
    {
        var project = await projectRepository.FindAsync(model.ProjectId);

        if (project == null)
            return Result.Failure($"Project with ID {model.ProjectId} not found", 400);

        var user = await userRepository.FindAsync(model.UserId);
        
        if (user == null)
            return Result.Failure($"User with ID {model.UserId} not found", 400);

        var comment = new ProjectComment(model.Content, model.ProjectId, model.UserId);

        await commentRepository.CreateAsync(comment);
        await commentRepository.CommitAsync();
        
        return Result.Success();
    }

    #endregion

    #region UPDATE

    public async Task<Result> Cancel(int projectId)
    {
        var project = await FindProject(projectId);

        if (project == null)
            return Result.Failure("Project not found");

        var result = project.Cancel();
        
        if (result.IsFailure)
            return Result.Failure(result.ErrorMessage, 400);
        
        await projectRepository.CommitAsync();

        return Result.Success();
    }

    public async Task<Result> Start(int projectId)
    {
        var project = await FindProject(projectId);

        if (project == null)
            return Result.Failure("Project not found");

        var result = project.Start();
        
        if (result.IsFailure)
            return Result.Failure(result.ErrorMessage, 400);
        
        await projectRepository.CommitAsync();

        return Result.Success();
    }

    public async Task<Result> Update(int projectId, UpdateProjectInputModel model)
    {
        var project = await FindProject(projectId);
        
        if (project == null)
            return Result.Failure($"Project with ID {projectId} not found", 400);

        project.Update(model.Title, model.Description, model.TotalCost);
        await projectRepository.CommitAsync();

        return Result.Success();
    }

    public async Task<Result> Finish(int projectId)
    {
        var project = await FindProject(projectId);
        
        if (project == null)
            return Result.Failure("Project not found");

        var result = project.Finish();
        
        if (result.IsFailure)
            return Result.Failure(result.ErrorMessage, 400);
        
        await projectRepository.CommitAsync();

        return Result.Success();
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
            Items = [.. projects.Items.Select(p => new ProjectViewModel(p.Id, p.Title, p.CreatedAt))],
        };

        return projectsResponse;
    }

    public async Task<Result<ProjectDetailsViewModel>> GetById(int id)
    {
        var project = await projectRepository.GetWithDetailsAsync(id);
        
        if (project == null)
            return Result.Failure<ProjectDetailsViewModel>($"Project with ID {id} not found", 404);

        var projectModel = new ProjectDetailsViewModel(project.Id, project.Title, project.TotalCost, project.Description,
            project.StartedAt, project.FinishedAt, project.CancelledAt, project.Status, project.Client!.FullName,
            project.Freelancer!.FullName);
        
        return Result.Success(projectModel);
    }

    #endregion

    #region Métodos

    private async Task<Project?> FindProject(int id)
    {
        return await projectRepository.FindAsync(id);
    }

    #endregion
}