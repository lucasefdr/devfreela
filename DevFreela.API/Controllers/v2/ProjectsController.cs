using DevFreela.Application.Commands.CancelProject;
using DevFreela.Application.Commands.CreateComment;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.FinishProject;
using DevFreela.Application.Commands.StartProject;
using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Application.Queries.GetProjectById;
using DevFreela.Application.ViewModels.Project;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers.v2;

[ApiController]
[Route("api/v2/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProjectsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProjectViewModel>>> Get()
    {
        var query = new GetAllProjectsQuery();
        var projects = await _mediator.Send(query);

        return Ok(projects);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectDetailsViewModel>> GetById(int id)
    {
        var query = new GetProjectByIdQuery(id);
        var project = await _mediator.Send(query);

        if (project is null) return NotFound("Project not found");

        return Ok(project);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CreateProjectCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { Id = id }, command);
    }

    [HttpPost("{id}/comments")]
    public async Task<ActionResult> PostComment(int id, CreateCommentCommand command)
    {
        if (id != command.IdProject)
            return BadRequest();

        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPut]
    public async Task<ActionResult> Put(int id, UpdateProjectCommand command)
    {
        if (id != command.Id)
            return BadRequest();

        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPut("{id}/start")]
    public async Task<ActionResult> Start(int id)
    {
        var command = new StartProjectCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPut("{id}/finish")]
    public async Task<ActionResult> Finish(int id)
    {
        var command = new FinishProjectCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPut("{id}/cancel")]
    public async Task<ActionResult> CancelProject(int id)
    {
        var command = new CancelProjectCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }

}
