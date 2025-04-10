using DevFreela.Application.DTOs.InputModels.Login;
using DevFreela.Core.Common;
using DevFreela.Core.Entities;

namespace DevFreela.Application.Repositories;

public interface IAuthRepository
{
    Task<Result> CreateAsync(RegisterUserInputModel model);
    Task<Result<(string, string)>> LoginAsync(string email, string password);
    Task<Result<(string, string)>> RefreshTokenAsync(string refreshToken, string accessToken);
}