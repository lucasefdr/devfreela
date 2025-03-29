using Asp.Versioning;
using DevFreela.Application.Common;
using DevFreela.Application.DTOs.InputModels.Skill;
using DevFreela.Application.DTOs.ViewModels.Skill;
using DevFreela.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers.v1;

[ApiVersion("1.0")]
[Route("api/v{v:apiVersion}/[controller]")]
[ApiController]
public class SkillsController(ISkillService skillService) : ControllerBase
{
    #region GET
    [HttpGet]
    public async Task<ActionResult<PagedResult<SkillViewModel>>> GetAll([FromQuery] QueryParameters parameters)
    {
        var skills = await skillService.GetAll(parameters);

        return Ok(skills);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<SkillViewModel>> GetByID(int id)
    {
        var skill = await skillService.GetById(id);

        return skill != null ? Ok(skill) : NotFound("Skill not found");
    }
    #endregion

    #region POST
    [HttpPost]
    public async Task<ActionResult> Post(SkillInputModel model)
    {
        var newId = await skillService.Create(model);
        return CreatedAtAction(nameof(GetByID), new { v = "1", id = newId }, model);
    }
    #endregion
}
