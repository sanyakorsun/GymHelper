using GymHelper.Application.DTOs;
using GymHelper.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace GymHelper.API.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly UserService _service;

    public UsersController(UserService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserDto dto)
    {
        var user = await _service.CreateAsync(dto);

        return Created($"/api/users/{user.Id}", user);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var user = await _service.GetByIdAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateUserDto dto)
    {
        var user = await _service.UpdateAsync(id, dto);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }
}