using Asp.Versioning;
using DevFreela.Application.Commands.SkillCommands;
using DevFreela.Application.Queries.SkillQueries.GetSkill;
using DevFreela.Application.Queries.SkillQueries.GetSkills;
using DevFreela.Application.ViewModels.Skill;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers.v2;

[ApiController]
[ApiVersion("2.0")]
[Route("api/v{v:apiVersion}/[controller]")]
public class SkillsController : ControllerBase
{
    private readonly IMediator _mediator;

    public SkillsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<SkillViewModel>>> GetAll()
    {
        var query = new GetSkillsQuery();
        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<SkillViewModel?>> Get(int id)
    {
        var query = new GetSkillQuery(id);
        var result = await _mediator.Send(query);

        return result is not null ? Ok(result) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateSkillCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(Get), new { v = "2.0", id = result }, command);
    }
}
