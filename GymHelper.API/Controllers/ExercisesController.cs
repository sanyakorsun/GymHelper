using GymHelper.Application.DTOs;
using GymHelper.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace GymHelper.API.Controllers;

[ApiController]
[Route("api/exercises")]
public class ExercisesController : ControllerBase
{
    private readonly ExerciseService _service;

    public ExercisesController(ExerciseService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var exercises = await _service.GetAllAsync();
        return Ok(exercises);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateExerciseDto dto)
    {
        var exercise = await _service.CreateAsync(dto);

        return Created($"/api/exercises/{exercise.Id}", exercise);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);

        return NoContent();
    }
}