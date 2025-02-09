using DevFreela.API.Configurations;
using DevFreela.API.Models;
using DevFreela.Application.InputModels.Project;
using DevFreela.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DevFreela.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectsController : ControllerBase
{
    private readonly OpeningTimeOption _option;
    private readonly IProjectService _projectService;

    public ProjectsController(IOptions<OpeningTimeOption> option, IProjectService projectService)
    {
        _option = option.Value;
        _projectService = projectService;
    }

    // api/projects?query=myQuery
    [HttpGet]
    public IActionResult Get(string query)
    {
        var projects = _projectService.GetAll(query);
        return Ok(projects);
    }

    // api/projects/{id}
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var project = _projectService.GetById(id);

        return project is not null ? Ok(project) : NotFound("Project not found");
    }

    // api/projects
    [HttpPost]
    public IActionResult Post([FromBody] NewProjectInputModel model)
    {
        var newId = _projectService.Create(model);

        // CreatedAtAction(methodName, anonObjWithParam, newObj)
        return CreatedAtAction(nameof(GetById), new { id = newId }, model);
    }

    // api/projects/{id}
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] UpdateProjectInputModel model)
    {
        if (id != model.Id)
        {
            return BadRequest();
        }

        _projectService.Update(model);

        return NoContent();
    }

    // api/projects/{id}
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _projectService.Delete(id);

        return NoContent();
    }

    // api/projects/{id}/comments
    [HttpPost("{id}/comments")]
    public IActionResult PostComment(int id, [FromBody] CreateCommentInputModel model)
    {
        _projectService.CreateComment(model);

        return NoContent();
    }

    // api/projects/{id}/start
    [HttpPut("{id}/start")]
    public IActionResult Start(int id)
    {
        _projectService.Start(id);
        return NoContent();
    }

    // api/projects/{id}/finish
    [HttpPut("{id}/finish")]
    public IActionResult Finish(int id)
    {
        _projectService.Finish(id);
        return NoContent();
    }

    [HttpGet("openingTimeOption")]
    public IActionResult GetOpeningTimeOption()
    {
        return Ok($"Start at: {_option.StartAt} - End at: {_option.EndAt}");
    }

}
