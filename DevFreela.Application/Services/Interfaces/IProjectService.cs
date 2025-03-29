using DevFreela.Application.Common;
using DevFreela.Application.DTOs.InputModels.Project;
using DevFreela.Application.DTOs.ViewModels.Project;
using DevFreela.Core.Common;
using DevFreela.Core.Entities;

namespace DevFreela.Application.Services.Interfaces;

public interface IProjectService
{
    Task<PagedResult<ProjectViewModel>> GetAll(QueryParameters parameters);
    Task<Result<ProjectDetailsViewModel>> GetById(int id);
    Task<Result<int>> Create(CreateProjectInputModel model);
    Task<Result> CreateComment(CreateCommentInputModel model);
    Task<Result> Start(int projectId);
    Task<Result> Finish(int projectId);
    Task<Result> Cancel(int projectId);
    Task<Result> Update(int projectId, UpdateProjectInputModel model);
}
