using DevFreela.Application.DTOs.InputModels.Login;
using DevFreela.Application.DTOs.ViewModels.Login;
using DevFreela.Core.Common;

namespace DevFreela.Application.Services.Interfaces;

public interface IAuthService
{
    Task<Result<TokenViewModel>> Login(LoginInputModel inputModel);
    Task<Result> Register(RegisterUserInputModel inputModel);
    Task<Result<TokenViewModel>> RefreshToken(string accessToken, string refreshToken);
}