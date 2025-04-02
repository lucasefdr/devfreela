using DevFreela.Application.Common;
using DevFreela.Application.DTOs.InputModels.Login;
using DevFreela.Application.DTOs.InputModels.User;
using DevFreela.Application.DTOs.ViewModels.Login;
using DevFreela.Application.DTOs.ViewModels.Project;
using DevFreela.Application.DTOs.ViewModels.User;
using DevFreela.Core.Common;
using DevFreela.Core.Entities;

namespace DevFreela.Application.Services.Interfaces;

public interface IUserService
{
    Task<PagedResult<UserViewModel>> GetAllFreelancers(QueryParameters parameters);
    Task<PagedResult<UserViewModel>> GetAllClients(QueryParameters parameters);
    Task<Result<UserViewModel>> GetById(int id);
    Task<int> Create(CreateUserInputModel inputModel);
    Task<Result> AddSkillToUser(int userId, int skillId);
    Task<Result> ActiveUser(int id);
    Task<Result> InactiveUser(int id);
    Task<Result<LoginViewModel>> Login(LoginInputModel inputModel);
}
