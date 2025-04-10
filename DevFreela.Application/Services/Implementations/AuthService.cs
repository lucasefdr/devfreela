using DevFreela.Application.DTOs.InputModels.Login;
using DevFreela.Application.DTOs.ViewModels.Login;
using DevFreela.Application.Repositories;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Core.Common;
using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace DevFreela.Application.Services.Implementations;

public class AuthService(IAuthRepository authRepository) : IAuthService
{
    public async Task<Result> Register(RegisterUserInputModel inputModel)
    {
        //var passwordHash = tokenService.ComputePasswordHash(inputModel.Password);

        var role = inputModel.UserType.Equals(UserTypeEnum.CLIENT)
            ? "client"
            : "freelancer";

        var result = await authRepository.CreateAsync(inputModel);

        return result.IsFailure
            ? Result.Failure(result.Error!)
            : Result.Success();
    }

    public async Task<Result<TokenViewModel>> Login(LoginInputModel inputModel)
    {
        var result = await authRepository.LoginAsync(inputModel.Email, inputModel.Password);

        return result.IsFailure
            ? Result.Failure<TokenViewModel>(result.Error!)
            : Result.Success(new TokenViewModel(result.Value.Item1, result.Value.Item2));
    }

    public async Task<Result<TokenViewModel>> RefreshToken(string accessToken, string refreshToken)
    {
        var result = await authRepository.RefreshTokenAsync(accessToken, refreshToken);
        
        return result.IsFailure
            ? Result.Failure<TokenViewModel>(result.Error!)
            : Result.Success(new TokenViewModel(result.Value.Item1, result.Value.Item2));
    }
}