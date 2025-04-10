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
            ? this.MapErrorToHttpResponse(result.Error!)
            : Ok(result.Value);
    }

    #endregion

    #region POST

    [HttpPost]
    public async Task<IActionResult> Post(CreateProjectInputModel model)
    {
        var result = await projectService.Create(model);

        return result.IsFailure
            ? this.MapErrorToHttpResponse(result.Error!)
            : NoContent();
    }

    [HttpPost("{id:int}/comments")]
    public async Task<IActionResult> PostComment(int id, CreateCommentInputModel model)
    {
        if (id != model.ProjectId)
            return BadRequest();

        var result = await projectService.CreateComment(model);

        return result.IsFailure
            ? this.MapErrorToHttpResponse(result.Error!)
            : NoContent();
    }

    #endregion

    #region PUT

    [HttpPut("{id:int}/start")]
    public async Task<IActionResult> Start(int id)
    {
        var result = await projectService.Start(id);

        return result.IsFailure
            ? this.MapErrorToHttpResponse(result.Error!)
            : NoContent();
    }

    [HttpPut("{id:int}/finish")]
    public async Task<IActionResult> Finish(int id)
    {
        var result = await projectService.Finish(id);

        return result.IsFailure
            ? this.MapErrorToHttpResponse(result.Error!)
            : NoContent();
    }

    [HttpPut("{id:int}/cancel")]
    public async Task<IActionResult> Cancel(int id)
    {
        var result = await projectService.Cancel(id);

        return result.IsFailure
            ? this.MapErrorToHttpResponse(result.Error!)
            : NoContent();
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, UpdateProjectInputModel model)
    {
        await projectService.Update(id, model);
        return NoContent();
    }

    #endregion
}