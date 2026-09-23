using GymHelper.Domain.Entities;

namespace GymHelper.Application.Interfaces;

public interface IWorkoutExerciseRepository
{
    Task<WorkoutExercise> AddAsync(WorkoutExercise workoutExercise);
}