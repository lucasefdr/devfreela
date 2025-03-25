using Asp.Versioning;
using DevFreela.Application.DTOs.ViewModels.User;
using DevFreela.Application.Features.Commands.UserCommands.AddSkillToUser;
using DevFreela.Application.Features.Commands.UserCommands.CreateUser;
using DevFreela.Application.Features.Queries.UserQueries.GetUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers.v2;

[ApiController]
[ApiVersion("2.0")]
[Route("api/v{v:apiVersion}/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserViewModel>> Get(int id)
    {
        var query = new GetUserQuery(id);
        var result = await _mediator.Send(query);

        return result is not null ? Ok(result) : NotFound("User not found");
    }

    [HttpPost]
    public async Task<ActionResult> Post(CreateUserCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(Get), new { v = "2.0", id = result }, command);
    }

    [HttpPut("{userId:int}/skill/{skillId:int}")]
    public async Task<ActionResult> AddSkillToUser(int userId, int skillId)
    {
        var command = new AddSkillToUserCommand(userId, skillId);
        await _mediator.Send(command);
        return NoContent();
    }
}
