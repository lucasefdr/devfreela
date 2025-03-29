using DevFreela.Application.Common;
using DevFreela.Application.DTOs.InputModels.User;
using DevFreela.Application.DTOs.ViewModels.User;
using DevFreela.Application.Repositories;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Core.Common;
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
            Items =
            [
                .. freelancersPaged.Items
                    .Select(f => new UserViewModel(f.Id, f.FullName, f.Email, f.IsActive.ToString(),
                        [.. f.UserSkills.Select(us => us.Skill!.Description)]))
            ]
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
            Items =
            [
                .. clientsPaged.Items
                    .Select(c => new UserViewModel(c.Id, c.FullName, c.Email, c.IsActive.ToString(),
                        [.. c.UserSkills.Select(us => us.Skill!.Description)]))
            ]
        };

        return clientsPagedResponse;
    }

    public async Task<Result<UserViewModel>> GetById(int id)
    {
        var user = await userRepository.GetUserWithSkillsAsync(id);

        if (user == null)
            return Result.Failure<UserViewModel>($"User with ID {id} not found.", 404);

        var userViewModel = new UserViewModel(
            user.Id, user.FullName, user.Email, user.IsActive.ToString(),
            [.. user.UserSkills.Select(us => us.Skill!.Description)]);

        return Result.Success(userViewModel);
    }

    #endregion

    #region UPDATE

    public async Task<Result> AddSkillToUser(int userId, int skillId)
    {
        var user = await userRepository.Get(userId);
        
        if (user == null)
            return Result.Failure($"User with ID {userId} not found", 404);

        var skill = await skillRepository.FindAsync(skillId);

        if (skill == null)
            return Result.Failure($"Skill with ID {skillId} not found", 404);
        
        var result = user.AddSkill(skill);

        if (result.IsFailure)
            return Result.Failure(result.ErrorMessage, 400);

        await userRepository.CommitAsync();

        return Result.Success();
    }

    public async Task<Result> ActiveUser(int id)
    {
        var user = await userRepository.FindAsync(id);

        if (user == null)
            return Result.Failure($"User with ID {id} not found", 404);

        var result = user.ActiveUser();

        if (result.IsFailure)
            return Result.Failure(result.ErrorMessage, 400);

        await userRepository.CommitAsync();
        return Result.Success();
    }

    public async Task<Result> InactiveUser(int id)
    {
        var user = await userRepository.FindAsync(id);

        if (user == null)
            return Result.Failure($"User with ID {id} not found", 404);

        var result = user.InactiveUser();
        
        if (result.IsFailure)
            return Result.Failure(result.ErrorMessage, 400);

        await userRepository.CommitAsync();
        return Result.Success();
    }

    #endregion

    #region CREATE

    public async Task<int> Create(CreateUserInputModel inputModel)
    {
        var newUser = new User(inputModel.FullName, inputModel.Email, inputModel.BirthDate, inputModel.UserType);
        await userRepository.CreateAsync(newUser);
        await userRepository.CommitAsync();
        return newUser.Id;
    }

    #endregion
}