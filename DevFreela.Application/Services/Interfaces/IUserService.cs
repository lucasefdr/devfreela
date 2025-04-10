using DevFreela.Application.Common;
using DevFreela.Application.DTOs.InputModels.Login;
using DevFreela.Application.DTOs.ViewModels.Login;
using DevFreela.Application.DTOs.ViewModels.Project;
using DevFreela.Application.DTOs.ViewModels.User;
using DevFreela.Core.Common;
using DevFreela.Core.Entities;

namespace DevFreela.Application.Services.Interfaces;

public interface IUserService
{
    Task<PagedResult<FreelancerViewModel>> GetAllFreelancers(QueryParameters parameters);
    Task<Result<FreelancerViewModel>> GetFreelancerWithDetails(int id);
    Task<PagedResult<ClientViewModel>> GetAllClients(QueryParameters parameters);
    Task<Result<ClientViewModel>> GetClientWithDetails(int id);
    
   
    Task<Result> AddSkillToUser(int userId, int skillId);
    Task<Result> ActiveUser(int id);
    Task<Result> InactiveUser(int id);
    

}
