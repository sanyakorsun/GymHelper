using GymHelper.Application.DTOs;
using GymHelper.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace GymHelper.API.Controllers;

[ApiController]
[Route("api/workout-exercises")]
public class WorkoutExercisesController : ControllerBase
{
    private readonly WorkoutExerciseService _service;

    public WorkoutExercisesController(WorkoutExerciseService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateWorkoutExerciseDto dto)
    {
        var workoutExercise = await _service.CreateAsync(dto);

        return Created($"/api/workout-exercises/{workoutExercise.Id}", workoutExercise);
    }
}