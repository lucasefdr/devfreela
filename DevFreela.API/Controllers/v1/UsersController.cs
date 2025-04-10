using Asp.Versioning;
using DevFreela.Application.Common;
using DevFreela.Application.DTOs.ViewModels.User;
using DevFreela.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DevFreela.API.Extensions;
using DevFreela.Application.DTOs.InputModels.Login;
using DevFreela.Application.DTOs.ViewModels.Login;
using Microsoft.AspNetCore.Authorization;

namespace DevFreela.API.Controllers.v1;

[ApiVersion("1.0")]
[Route("api/v{v:apiVersion}/[controller]")]
[ApiController]
[Authorize]
public class UsersController(IUserService userService) : ControllerBase
{
    #region GET

    [HttpGet("freelancers")]
    public async Task<IActionResult> GetAllFreelancers([FromQuery] QueryParameters parameters)
    {
        var freelancers = await userService.GetAllFreelancers(parameters);
        return Ok(freelancers);
    }

    [HttpGet("freelancers/{id:int}")]
    public async Task<IActionResult> GetFreelancer(int id)
    {
        var result = await userService.GetFreelancerWithDetails(id);

        return result.IsFailure
            ? this.MapErrorToHttpResponse(result.Error!)
            : Ok(result.Value);
    }

    [HttpGet("clients")]
    public async Task<IActionResult> GetAllClients([FromQuery] QueryParameters parameters)
    {
        var clients = await userService.GetAllClients(parameters);
        return Ok(clients);
    }

    [HttpGet("clients/{id:int}")]
    public async Task<IActionResult> GetClient(int id)
    {
        var result = await userService.GetClientWithDetails(id);

        return result.IsFailure
            ? this.MapErrorToHttpResponse(result.Error!)
            : Ok(result.Value);
    }

    #endregion



    #region PUT

    [HttpPut("freelancers/{userId:int}/skill/{skillId:int}")]
    public async Task<IActionResult> AddSkillToFreelancer(int userId, int skillId)
    {
        var result = await userService.AddSkillToUser(userId, skillId);

        return result.IsFailure
            ? this.MapErrorToHttpResponse(result.Error!)
            : NoContent();
    }

    [HttpPut("{id:int}/active")]
    public async Task<IActionResult> ActiveUser(int id)
    {
        var result = await userService.ActiveUser(id);

        return result.IsFailure
            ? this.MapErrorToHttpResponse(result.Error!)
            : NoContent();
    }

    [HttpPut("{id:int}/inactive")]
    public async Task<IActionResult> InactiveUser(int id)
    {
        var result = await userService.InactiveUser(id);

        return result.IsFailure
            ? this.MapErrorToHttpResponse(result.Error!)
            : NoContent();
    }

    #endregion
}