using Asp.Versioning;
using DevFreela.API.Extensions;
using DevFreela.Application.Common;
using DevFreela.Application.DTOs.InputModels.Project;
using DevFreela.Application.DTOs.ViewModels.Project;
using DevFreela.Application.Services.Implementations;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers.v1;

[ApiVersion("1.0")]
[Route("api/v{v:apiVersion}/[controller]")]
[ApiController]
public class ProjectsController(IProjectService projectService) : ControllerBase
{
    #region GET
    [HttpGet]
    public async Task<ActionResult<PagedResult<Project>>> GetAll([FromQuery] QueryParameters parameters)
    {
        var result = await projectService.GetAll(parameters);
        Response.AddPaginationHeaders(result);

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProjectDetailsViewModel>> GetByID(int id)
    {
        var project = await projectService.GetByID(id);
        return project != null ? Ok(project) : NotFound("User not found");
    }
    #endregion

    #region POST
    [HttpPost]
    public async Task<ActionResult> Post(CreateProjectInputModel model)
    {
        var newId = await projectService.Create(model);
        return CreatedAtAction(nameof(GetByID), new { v = "1", id = newId }, model);
    }

    [HttpPost("{id:int}/comments")]
    public async Task<ActionResult> PostComment(int id, CreateCommentInputModel model)
    {
        if (id != model.ProjectID)
            return BadRequest();

        await projectService.CreateComment(model);
        return NoContent();
    }
    #endregion

    #region PUT
    [HttpPut("{id:int}/start")]
    public async Task<ActionResult> Start(int id)
    {
        await projectService.Start(id);
        return NoContent();
    }

    [HttpPut("{id:int}/finish")]
    public async Task<ActionResult> Finish(int id)
    {
        await projectService.Finish(id);
        return NoContent();
    }

    [HttpPut("{id:int}/cancel")]
    public async Task<ActionResult> Cancel(int id)
    {
        await projectService.Cancel(id);
        return NoContent();
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, UpdateProjectInputModel model)
    {
        await projectService.Update(id, model);
        return NoContent();
    }
    #endregion

}
