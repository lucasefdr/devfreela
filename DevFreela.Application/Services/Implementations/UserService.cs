using DevFreela.Application.Common;
using DevFreela.Application.DTOs.InputModels.Login;
using DevFreela.Application.DTOs.InputModels.User;
using DevFreela.Application.DTOs.ViewModels.Login;
using DevFreela.Application.DTOs.ViewModels.User;
using DevFreela.Application.Interfaces;
using DevFreela.Application.Repositories;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Core.Common;
using DevFreela.Core.Entities;
using DevFreela.Core.Enums;

namespace DevFreela.Application.Services.Implementations;

public class UserService(
    IUserRepository userRepository,
    ISkillRepository skillRepository,
    ITokenService tokenService) : IUserService
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
            return Result.Failure<UserViewModel>($"User with Id {id} not found.", 404);

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
            return Result.Failure($"User with Id {userId} not found", 404);

        var skill = await skillRepository.FindAsync(skillId);

        if (skill == null)
            return Result.Failure($"Skill with Id {skillId} not found", 404);

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
            return Result.Failure($"User with Id {id} not found", 404);

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
            return Result.Failure($"User with Id {id} not found", 404);

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
        var passwordHash = tokenService.ComputePasswordHash(inputModel.Password);

        var role = inputModel.UserType.Equals(UserTypeEnum.CLIENT)
            ? "client"
            : "freelancer";

        var newUser = new User(
            inputModel.FullName,
            inputModel.Email,
            inputModel.BirthDate,
            inputModel.UserType,
            passwordHash,
            role);

        await userRepository.CreateAsync(newUser);
        await userRepository.CommitAsync();
        return newUser.Id;
    }

    public async Task<Result<LoginViewModel>> Login(LoginInputModel inputModel)
    {
        var passwordHash = tokenService.ComputePasswordHash(inputModel.Password);

        var user = await userRepository.LoginAsync(inputModel.Email, passwordHash);

        if (user == null)
            return Result.Failure<LoginViewModel>("Invalid credentials", 400);

        var token = tokenService.GenerateJwtToken(user.Email, user.FullName, user.Role);
        var response = new LoginViewModel(token);

        return Result.Success(response);
    }

    #endregion
}