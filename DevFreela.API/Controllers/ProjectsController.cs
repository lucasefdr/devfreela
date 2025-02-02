using DevFreela.API.Configurations;
using DevFreela.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DevFreela.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectsController : ControllerBase
{
    private readonly OpeningTimeOption _option;
    public ProjectsController(IOptions<OpeningTimeOption> option)
    {
        _option = option.Value;
    }
    // api/projects?query=myQuery
    [HttpGet]
    public IActionResult Get(string query)
    {
        return Ok();
    }

    // api/projects/{id}
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        //return NotFound();
        return Ok();
    }

    // api/projects
    [HttpPost]
    public IActionResult Post([FromBody] CreateProjectModel model)
    {
        // Validation example 
        if (model.Title.Length > 50)
        {
            return BadRequest();
        }

        // CreatedAtAction(methodName, anonObjWithParam, newObj)
        return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
    }

    // api/projects/{id}
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] UpdateProjectModel model)
    {
        // return NotFound();
        if (model.Description.Length > 200)
        {
            return BadRequest();
        }

        return NoContent();
    }

    // api/projects/{id}
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        // return NotFound();
        return NoContent();
    }

    // api/projects/{id}/comments
    [HttpPost("{id}/comments")]
    public IActionResult PostComment(int id, [FromBody] CreateCommentModel model)
    {
        return NoContent();
    }

    // api/projects/{id}/start
    [HttpPut("{id}/start")]
    public IActionResult Start(int id)
    {
        return NoContent();
    }

    // api/projects/{id}/finish
    [HttpPut("{id}/finish")]
    public IActionResult Finish(int id)
    {
        return NoContent();
    }

    [HttpGet("openingTimeOption")]
    public IActionResult GetOpeningTimeOption()
    {
        return Ok($"Start at: {_option.StartAt} - End at: {_option.EndAt}");
    }

}
