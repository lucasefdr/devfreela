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
    public async Task<IActionResult> GetAll([FromQuery] QueryParameters parameters)
    {
        var result = await projectService.GetAll(parameters);
        
        Response.AddPaginationHeaders(result);

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await projectService.GetById(id);

        return result.IsFailure
            ? NotFound(new { error = result.ErrorMessage })
            : Ok(result.Value);
    }

    #endregion

    #region POST

    [HttpPost]
    public async Task<ActionResult> Post(CreateProjectInputModel model)
    {
        var result = await projectService.Create(model);

        return result.IsFailure
            ? StatusCode(result.StatusCode, new { error = result.ErrorMessage })
            : CreatedAtAction(nameof(GetById), new { v = "1", id = result.Value }, model);
    }

    [HttpPost("{id:int}/comments")]
    public async Task<ActionResult> PostComment(int id, CreateCommentInputModel model)
    {
        if (id != model.ProjectId)
            return BadRequest();

        var result = await projectService.CreateComment(model);

        return result.IsFailure
            ? StatusCode(result.StatusCode, new { error = result.ErrorMessage })
            : NoContent();
    }

    #endregion

    #region PUT

    [HttpPut("{id:int}/start")]
    public async Task<ActionResult> Start(int id)
    {
        var result = await projectService.Start(id);

        if (result.IsFailure)
            return StatusCode(result.StatusCode, new { error = result.ErrorMessage });

        return NoContent();
    }

    [HttpPut("{id:int}/finish")]
    public async Task<ActionResult> Finish(int id)
    {
        var result = await projectService.Finish(id);

        if (result.IsFailure)
            return StatusCode(result.StatusCode, new { error = result.ErrorMessage });

        return NoContent();
    }

    [HttpPut("{id:int}/cancel")]
    public async Task<ActionResult> Cancel(int id)
    {
        var result = await projectService.Cancel(id);

        if (result.IsFailure)
            return StatusCode(result.StatusCode, new { error = result.ErrorMessage });

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