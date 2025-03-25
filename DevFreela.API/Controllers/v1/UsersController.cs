using Asp.Versioning;
using DevFreela.Application.Common;
using DevFreela.Application.DTOs.InputModels.User;
using DevFreela.Application.DTOs.ViewModels.User;
using DevFreela.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DevFreela.API.Controllers.v1;


[ApiVersion("1.0")]
[Route("api/v{v:apiVersion}/[controller]")]
[ApiController]
public class UsersController(IUserService userService) : ControllerBase
{
    #region GET
    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserViewModel>> GetByID(int id)
    {
        var user = await userService.GetByID(id);
        return user != null ? Ok(user) : NotFound("User not found");
    }

    [HttpGet("freelancers")]
    public async Task<ActionResult<PagedResult<UserViewModel>>> GetAllFreelancers([FromQuery] QueryParameters parameters)
    {
        var freelancers = await userService.GetAllFreelancers(parameters);
        return Ok(freelancers);
    }

    [HttpGet("clients")]
    public async Task<ActionResult<PagedResult<UserViewModel>>> GetAllClients([FromQuery] QueryParameters parameters)
    {
        var clients = await userService.GetAllClients(parameters);
        return Ok(clients);
    }
    #endregion

    #region POST
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CreateUserInputModel model)
    {
        var newId = await userService.Create(model);
        return CreatedAtAction(nameof(GetByID), new { v = "1", id = newId }, model);
    }
    #endregion

    #region PUT
    [HttpPut("{userID:int}/skill/{skillID:int}")]
    public async Task<ActionResult> AddSkillToUser(int userID, int skillID)
    {
        await userService.AddSkillToUser(userID, skillID);
        return NoContent();
    }

    [HttpPut("{id}/active")]
    public async Task<ActionResult> ActiveUser(int id)
    {
        await userService.ActiveUser(id);
        return NoContent();
    }

    [HttpPut("{id}/inactive")]
    public async Task<ActionResult> InactiveUser(int id)
    {
        await userService.InactiveUser(id);
        return NoContent();
    }
    #endregion

}
