using Asp.Versioning;
using DevFreela.Application.Commands.ProjectCommands.CancelProject;
using DevFreela.Application.Commands.ProjectCommands.CreateComment;
using DevFreela.Application.Commands.ProjectCommands.CreateProject;
using DevFreela.Application.Commands.ProjectCommands.FinishProject;
using DevFreela.Application.Commands.ProjectCommands.StartProject;
using DevFreela.Application.Commands.ProjectCommands.UpdateProject;
using DevFreela.Application.Queries.ProjectQueries.GetAllProjects;
using DevFreela.Application.Queries.ProjectQueries.GetProject;
using DevFreela.Application.Queries.ProjectQueries.GetProjectComments;
using DevFreela.Application.ViewModels.Project;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers.v2;

[ApiController]
[ApiVersion("2.0")]
[Route("api/v{v:apiVersion}/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProjectsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProjectViewModel>>> GetAll()
    {
        var query = new GetAllProjectsQuery();
        var projects = await _mediator.Send(query);

        return Ok(projects);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectDetailsViewModel>> Get(int id)
    {
        var query = new GetProjectQuery(id);
        var project = await _mediator.Send(query);

        if (project is null) return NotFound("Project not found");

        return Ok(project);
    }

    [HttpGet("{id}/comments")]
    public async Task<ActionResult<ProjectCommentsViewModel>> GetProjectComments(int id)
    {
        var query = new GetProjectCommentsQuery(id);
        var result = await _mediator.Send(query);

        return result is not null ? Ok(result) : NotFound("Project not found.");
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CreateProjectCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(Get), new { v = "2.0", id = result }, command);
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
