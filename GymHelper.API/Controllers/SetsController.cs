using GymHelper.Application.DTOs;
using GymHelper.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace GymHelper.API.Controllers;

[ApiController]
[Route("api/sets")]
public class SetsController : ControllerBase
{
    private readonly SetService _service;

    public SetsController(SetService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateSetDto dto)
    {
        var set = await _service.CreateAsync(dto);

        return Created($"/api/sets/{set.Id}", set);
    }
}