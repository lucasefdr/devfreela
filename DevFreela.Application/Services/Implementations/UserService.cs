using DevFreela.Application.Common;
using DevFreela.Application.DTOs.InputModels.Login;
using DevFreela.Application.DTOs.ViewModels.User;
using DevFreela.Application.Repositories;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Core.Common;
using DevFreela.Core.Entities;
using DevFreela.Core.Enums;

namespace DevFreela.Application.Services.Implementations;

public class UserService(
    IUserRepository userRepository,
    ISkillRepository skillRepository) : IUserService
{
    #region GET

    public async Task<PagedResult<FreelancerViewModel>> GetAllFreelancers(QueryParameters parameters)
    {
        var freelancersPaged = await userRepository.GetFreelancersAsync(parameters);

        var freelancersPagedResponse = new PagedResult<FreelancerViewModel>
        {
            CurrentPage = freelancersPaged.CurrentPage,
            PageSize = freelancersPaged.PageSize,
            TotalCount = freelancersPaged.TotalCount,
            TotalPages = freelancersPaged.TotalPages,
            Items =
            [
                .. freelancersPaged.Items
                    .Select(f => new FreelancerViewModel(
                        f.Id,
                        f.FullName,
                        f.IsActive.ToString(),
                        [.. f.UserSkills.Select(us => us.Skill!.Description)]))
            ]
        };

        return freelancersPagedResponse;
    }

    public async Task<Result<FreelancerViewModel>> GetFreelancerWithDetails(int id)
    {
        var freelancer = await userRepository.GetFreelancerWithDetailsAsync(id);

        if (freelancer is null)
            return Result.Failure<FreelancerViewModel>(Error.NotFound("User.NotFound", "Freelancer not found."));

        var response = new FreelancerViewModel(
            freelancer.Id,
            freelancer.FullName,
            freelancer.IsActive.ToString(),
            [.. freelancer.UserSkills.Select(us => us.Skill!.Description)]);

        return Result.Success(response);
    }

    public async Task<PagedResult<ClientViewModel>> GetAllClients(QueryParameters parameters)
    {
        var clientsPaged = await userRepository.GetClientsAsync(parameters);

        var clientsPagedResponse = new PagedResult<ClientViewModel>
        {
            CurrentPage = clientsPaged.CurrentPage,
            PageSize = clientsPaged.PageSize,
            TotalCount = clientsPaged.TotalCount,
            TotalPages = clientsPaged.TotalPages,
            Items =
            [
                .. clientsPaged.Items
                    .Select(c => new ClientViewModel(
                        c.Id,
                        c.FullName,
                        c.IsActive.ToString(),
                        [.. c.UserSkills.Select(us => us.Skill!.Description)]))
            ]
        };

        return clientsPagedResponse;
    }

    public async Task<Result<ClientViewModel>> GetClientWithDetails(int id)
    {
        var freelancer = await userRepository.GetFreelancerWithDetailsAsync(id);

        if (freelancer is null)
            return Result.Failure<ClientViewModel>(Error.NotFound("User.NotFound", "Freelancer not found."));

        var response = new ClientViewModel(
            freelancer.Id,
            freelancer.FullName,
            freelancer.IsActive.ToString(),
            [.. freelancer.UserSkills.Select(us => us.Skill!.Description)]);

        return Result.Success(response);
    }

    #endregion

    #region UPDATE

    public async Task<Result> AddSkillToUser(int userId, int skillId)
    {
        var user = await FindUserOrFail(userId);

        if (user.IsFailure)
            return Result.Failure<UserViewModel>(user.Error!);

        var skill = await skillRepository.FindAsync(skillId);

        if (skill == null)
            return Result.Failure(Error.NotFound("Skill", $"Skill with Id {skillId} not found."));

        var result = user.Value.AddSkill(skill);

        if (result.IsFailure)
            return Result.Failure(result.Error!);

        await userRepository.CommitAsync();

        return Result.Success();
    }

    public async Task<Result> ActiveUser(int id)
    {
        var user = await FindUserOrFail(id);

        if (user.IsFailure)
            return Result.Failure(user.Error!);

        var result = user.Value.ActiveUser();

        if (result.IsFailure)
            return Result.Failure(user.Error!);

        await userRepository.CommitAsync();
        return Result.Success();
    }

    public async Task<Result> InactiveUser(int id)
    {
        var user = await FindUserOrFail(id);

        if (user.IsFailure)
            return Result.Failure(user.Error!);

        var result = user.Value.InactiveUser();

        if (result.IsFailure)
            return Result.Failure(result.Error!);

        await userRepository.CommitAsync();
        return Result.Success();
    }

    #endregion


    #region METHODS

    private async Task<Result<User>> FindUserOrFail(int id)
    {
        var user = await userRepository.FindAsync(id);

        return user is null
            ? Result.Failure<User>(Error.NotFound("User", $"User with ID {id} not found."))
            : Result.Success(user);
    }

    #endregion
}