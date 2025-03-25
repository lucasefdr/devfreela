using DevFreela.Application.Common;
using DevFreela.Application.DTOs.InputModels.User;
using DevFreela.Application.DTOs.ViewModels.User;
using DevFreela.Application.Repositories;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Core.Entities;

namespace DevFreela.Application.Services.Implementations;

public class UserService(IUserRepository userRepository, ISkillRepository skillRepository) : IUserService
{

    #region GET
    public async Task<PagedResult<UserViewModel>> GetAllFreelancers(QueryParameters parameters)
    {
        var freelancersPaged = await userRepository.GetFreelancersAsync(parameters);

        var freelancersPagedResponse = new PagedResult<UserViewModel>
        {
            CurrentPage = freelancersPaged.CurrentPage,
            PageSize = freelancersPaged.PageSize,
            TotalCount = freelancersPaged.TotalCount,
            TotalPages = freelancersPaged.TotalPages,
            Items = [.. freelancersPaged.Items
                         .Select(f => new UserViewModel(f.ID, f.FullName, f.Email, f.IsActive.ToString() ,[.. f.UserSkills.Select(us => us.Skill!.Description)]))]
        };

        return freelancersPagedResponse;
    }

    public async Task<PagedResult<UserViewModel>> GetAllClients(QueryParameters parameters)
    {
        var clientsPaged = await userRepository.GetClientsAsync(parameters);

        var clientsPagedResponse = new PagedResult<UserViewModel>
        {
            CurrentPage = clientsPaged.CurrentPage,
            PageSize = clientsPaged.PageSize,
            TotalCount = clientsPaged.TotalCount,
            TotalPages = clientsPaged.TotalPages,
            Items = [.. clientsPaged.Items
                         .Select(c => new UserViewModel(c.ID, c.FullName, c.Email, c.IsActive.ToString(), [.. c.UserSkills.Select(us => us.Skill!.Description)]))]
        };

        return clientsPagedResponse;
    }

    public async Task<UserViewModel?> GetByID(int id)
    {
        var user = await userRepository.FindAsync(id);

        if (user == null) return null;

        return new UserViewModel(user.ID, user.FullName, user.Email, user.IsActive.ToString(), [.. user.UserSkills.Select(us => us.Skill!.Description)]);
    }
    #endregion

    #region UPDATE
    public async Task AddSkillToUser(int userId, int skillId)
    {
        var user = await userRepository.FindAsync(userId) ?? throw new KeyNotFoundException("User not found");
        var skill = await skillRepository.FindAsync(skillId) ?? throw new KeyNotFoundException("Skill not found");

        user.AddSkill(skill);

        await userRepository.CommitAsync();
    }

    public async Task ActiveUser(int id)
    {
        var user = await userRepository.FindAsync(id) ?? throw new KeyNotFoundException("User not found");

        user.ActiveUser();

        await userRepository.CommitAsync();
    }

    public async Task InactiveUser(int id)
    {
        var user = await userRepository.FindAsync(id) ?? throw new KeyNotFoundException("User not found");

        user.InactiveUser();

        await userRepository.CommitAsync();
    }
    #endregion

    #region CREATE
    public async Task<int> Create(CreateUserInputModel inputModel)
    {
        var newUser = new User(inputModel.FullName, inputModel.Email, inputModel.BirthDate, inputModel.UserType);
        await userRepository.CreateAsync(newUser);
        await userRepository.CommitAsync();
        return newUser.ID;
    }
    #endregion



}
