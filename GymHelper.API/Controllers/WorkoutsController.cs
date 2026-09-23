using GymHelper.Application.DTOs;
using GymHelper.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace GymHelper.API.Controllers;

[ApiController]
[Route("api/workouts")]
public class WorkoutsController : ControllerBase
{
    private readonly WorkoutService _service;

    public WorkoutsController(WorkoutService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateWorkoutDto dto)
    {
        var workout = await _service.CreateAsync(dto);

        return Created($"/api/workouts/{workout.Id}", workout);
    }

    [HttpPost("{id}/finish")]
    public async Task<IActionResult> Finish(Guid id)
    {
        var finished = await _service.FinishAsync(id);

        if (!finished)
        {
            return BadRequest("Тренування не містить жодного збереженого підходу");
        }

        return Ok(new
        {
            message = "Тренування завершено"
        });
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetAll(Guid userId)
    {
        var workouts = await _service.GetAllAsync(userId);

        return Ok(workouts);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);

        return NoContent();
    }
}