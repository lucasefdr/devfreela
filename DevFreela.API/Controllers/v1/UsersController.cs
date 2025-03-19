using Asp.Versioning;
using DevFreela.API.Models;
using DevFreela.Application.InputModels.User;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers.v1;


[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        return Ok(_userService.GetUser(id));
    }

    [HttpPost]
    public IActionResult Post([FromBody] CreateUserInputModel model)
    {
        var newId = _userService.Create(model);
        return CreatedAtAction(nameof(GetById), new { id = newId }, model);
    }

    [HttpPut("{id}")]
    public IActionResult Login(int id, [FromBody] LoginModel model)
    {
        return NoContent();
    }
}
