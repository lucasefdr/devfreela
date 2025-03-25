using DevFreela.Application.Common;
using DevFreela.Application.DTOs.InputModels.User;
using DevFreela.Application.DTOs.ViewModels.Project;
using DevFreela.Application.DTOs.ViewModels.User;
using DevFreela.Core.Entities;

namespace DevFreela.Application.Services.Interfaces;

public interface IUserService
{
    Task<PagedResult<UserViewModel>> GetAllFreelancers(QueryParameters parameters);
    Task<PagedResult<UserViewModel>> GetAllClients(QueryParameters parameters);
    Task<UserViewModel?> GetByID(int id);
    Task<int> Create(CreateUserInputModel inputModel);
    Task AddSkillToUser(int userId, int skillId);
    Task ActiveUser(int id);
    Task InactiveUser(int id);
}
