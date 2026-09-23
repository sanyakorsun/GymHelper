using GymHelper.Application.DTOs;
using GymHelper.Application.Interfaces;
using GymHelper.Domain.Entities;

namespace GymHelper.Application.Services;

public class WorkoutExerciseService
{
    private readonly IWorkoutExerciseRepository _repository;

    public WorkoutExerciseService(IWorkoutExerciseRepository repository)
    {
        _repository = repository;
    }

    public async Task<WorkoutExercise> CreateAsync(CreateWorkoutExerciseDto dto)
    {
        var workoutExercise = new WorkoutExercise
        {
            WorkoutId = dto.WorkoutId,
            ExerciseId = dto.ExerciseId
        };

        return await _repository.AddAsync(workoutExercise);
    }
}