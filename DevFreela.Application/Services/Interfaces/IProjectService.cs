using DevFreela.Application.Common;
using DevFreela.Application.DTOs.InputModels.Project;
using DevFreela.Application.DTOs.ViewModels.Project;
using DevFreela.Core.Entities;

namespace DevFreela.Application.Services.Interfaces;

public interface IProjectService
{
    Task<PagedResult<ProjectViewModel>> GetAll(QueryParameters parameters);
    Task<ProjectDetailsViewModel?> GetByID(int id);
    Task<int> Create(CreateProjectInputModel model);
    Task CreateComment(CreateCommentInputModel model);
    Task Start(int projectID);
    Task Finish(int projectID);
    Task Cancel(int projectID);
    Task Update(int projectID, UpdateProjectInputModel model);
}
