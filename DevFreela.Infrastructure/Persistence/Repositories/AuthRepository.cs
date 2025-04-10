using System.Security.Claims;
using DevFreela.Application.DTOs.InputModels.Login;
using DevFreela.Application.Repositories;
using DevFreela.Core.Common;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Auth;
using DevFreela.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;

namespace DevFreela.Infrastructure.Persistence.Repositories;

public class AuthRepository(
    UserManager<ApplicationUser> manager,
    IRepository<User> repository,
    ITokenService tokenService,
    IConfiguration configuration) : IAuthRepository
{
    public async Task<Result> CreateAsync(RegisterUserInputModel model)
    {
        // Cria um ApplicationUser que serve como cadastro para login
        var applicationUser = new ApplicationUser
        {
            UserName = model.UserName,
            Email = model.Email,
            CreatedAt = DateTime.Now,
            SecurityStamp = Guid.NewGuid().ToString(),
        };

        var result = await manager.CreateAsync(applicationUser, model.Password);

        // Se o Identity não conseguir criar, lança um erro
        if (!result.Succeeded)
            return Result.Failure(Error.Validation("Auth.Register", result.Errors.First().Description));

        var user = new User(applicationUser.Id, model.FullName, model.BirthDate, model.UserType);

        // Cria o usuário do domínio
        await repository.CreateAsync(user);
        await repository.CommitAsync();

        return Result.Success();
    }

    public async Task<Result<(string, string)>> LoginAsync(string email, string password)
    {
        // Procura o usuário pelo email
        var user = await manager.FindByEmailAsync(email);

        // Valida se o usuário existe e se a senha está correta
        if (user is null || !await manager.CheckPasswordAsync(user, password))
            return Result.Failure<(string, string)>(Error.Validation("Auth.Login", "Email or password is incorrect"));

        // Busca as roles do usuário
        var userRoles = await manager.GetRolesAsync(user);

        // Adiciona claims de autenticação
        var authClaims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName!),
            new(ClaimTypes.Email, user.Email!),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        // Adiciona as roles como claims
        authClaims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

        // Gera o token
        var token = tokenService.GenerateJwtToken(authClaims);
        var refreshToken = tokenService.GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(Convert.ToDouble(configuration["Jwt:RefreshInMinutes"]));

        await manager.UpdateAsync(user);

        return Result.Success<(string, string)>((token, refreshToken));
    }

    public async Task<Result<(string, string)>> RefreshTokenAsync(string refreshToken, string accessToken)
    {
        var principal = tokenService.GetPrincipalFromExpiredToken(accessToken);

        if (principal is null)
            return Result.Failure<(string, string)>
                (Error.Validation("Auth.RefreshToken", "Refresh or access token is invalid"));

        var userEmail = principal.FindFirst(x => x.Type == ClaimTypes.Email)?.Value;

        var user = await manager.FindByEmailAsync(userEmail!);

        if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime < DateTime.Now)
            return Result.Failure<(string, string)>
                (Error.Validation("Auth.RefreshToken", "Refresh or access token is invalid"));

        var newAccessToken = tokenService.GenerateJwtToken(principal.Claims);
        user.RefreshToken = newAccessToken;
        await manager.UpdateAsync(user);

        return Result.Success<(string, string)>((accessToken, newAccessToken));
    }
}