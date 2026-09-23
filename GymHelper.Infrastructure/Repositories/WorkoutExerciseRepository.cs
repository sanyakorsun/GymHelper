using GymHelper.Application.Interfaces;
using GymHelper.Domain.Entities;
using GymHelper.Infrastructure.Data;

namespace GymHelper.Infrastructure.Repositories;

public class WorkoutExerciseRepository : IWorkoutExerciseRepository
{
    private readonly ApplicationDbContext _context;

    public WorkoutExerciseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<WorkoutExercise> AddAsync(WorkoutExercise workoutExercise)
    {
        _context.WorkoutExercises.Add(workoutExercise);
        await _context.SaveChangesAsync();

        return workoutExercise;
    }
}