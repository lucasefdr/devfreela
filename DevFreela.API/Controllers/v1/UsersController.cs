using Asp.Versioning;
using DevFreela.Application.Common;
using DevFreela.Application.DTOs.InputModels.User;
using DevFreela.Application.DTOs.ViewModels.User;
using DevFreela.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
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
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await userService.GetById(id);
        return result.IsFailure ? StatusCode(result.StatusCode, result.ErrorMessage) : Ok(result.Value);
    }

    [HttpGet("freelancers")]
    public async Task<IActionResult> GetAllFreelancers([FromQuery] QueryParameters parameters)
    {
        var freelancers = await userService.GetAllFreelancers(parameters);
        return Ok(freelancers);
    }

    [HttpGet("clients")]
    [Authorize(Roles = "client")]
    public async Task<IActionResult> GetAllClients([FromQuery] QueryParameters parameters)
    {
        var clients = await userService.GetAllClients(parameters);
        return Ok(clients);
    }
    #endregion

    #region POST
    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult> Post([FromBody] CreateUserInputModel model)
    {
        var newId = await userService.Create(model);
        return CreatedAtAction(nameof(GetById), new { v = "1", id = newId }, model);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<LoginViewModel>> Login([FromBody] LoginInputModel model)
    {
        var result = await userService.Login(model);
        
        return result.IsFailure ? StatusCode(result.StatusCode, result.ErrorMessage) : Ok(result.Value);
    }
    #endregion

    #region PUT
    [HttpPut("{userId:int}/skill/{skillId:int}")]
    public async Task<ActionResult> AddSkillToUser(int userId, int skillId)
    {
        var result = await userService.AddSkillToUser(userId, skillId);
        return result.IsFailure ? StatusCode(result.StatusCode, result.ErrorMessage) : NoContent();
    }

    [HttpPut("{id:int}/active")]
    public async Task<ActionResult> ActiveUser(int id)
    {
        var result = await userService.ActiveUser(id);
        return result.IsFailure ? StatusCode(result.StatusCode, result.ErrorMessage) : NoContent();
    }

    [HttpPut("{id:int}/inactive")]
    public async Task<ActionResult> InactiveUser(int id)
    {
        var result = await userService.InactiveUser(id);
        return result.IsFailure ? StatusCode(result.StatusCode, result.ErrorMessage) : NoContent();
    }
    #endregion

}
