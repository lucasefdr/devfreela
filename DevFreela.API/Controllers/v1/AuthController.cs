using Asp.Versioning;
using DevFreela.API.Extensions;
using DevFreela.Application.DTOs.InputModels.Login;
using DevFreela.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers.v1;

[ApiVersion("1.0")]
[Route("api/v{v:apiVersion}/[controller]")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserInputModel model)
    {
        var result = await authService.Register(model);

        return result.IsFailure
            ? this.MapErrorToHttpResponse(result.Error!)
            : NoContent();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginInputModel model)
    {
        var result = await authService.Login(model);

        return result.IsFailure
            ? this.MapErrorToHttpResponse(result.Error!)
            : Ok(result.Value);
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken(string accessToken, string refreshToken)
    {
        var result = await authService.RefreshToken(accessToken, refreshToken);
        
        return result.IsFailure
            ? this.MapErrorToHttpResponse(result.Error!)
            : Ok(result.Value);
    }
}