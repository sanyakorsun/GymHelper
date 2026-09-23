using GymHelper.Application.Interfaces;
using GymHelper.Domain.Entities;
using GymHelper.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GymHelper.Infrastructure.Repositories;

public class WorkoutRepository : IWorkoutRepository
{
    private readonly ApplicationDbContext _context;

    public WorkoutRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Workout> AddAsync(Workout workout)
    {
        _context.Workouts.Add(workout);
        await _context.SaveChangesAsync();

        return workout;
    }

    public async Task<bool> HasSetsAsync(Guid workoutId)
    {
        var count = await _context.Sets
            .Where(s => _context.WorkoutExercises
                .Any(we =>
                    we.Id == s.WorkoutExerciseId &&
                    we.WorkoutId == workoutId))
            .CountAsync();

        

        return count > 0;
    }

    public async Task DeleteAsync(Guid workoutId)
    {
        var workoutExercises = await _context.WorkoutExercises
            .Where(we => we.WorkoutId == workoutId)
            .ToListAsync();

        var workoutExerciseIds = workoutExercises
            .Select(we => we.Id)
            .ToList();

        var sets = await _context.Sets
            .Where(s => workoutExerciseIds.Contains(s.WorkoutExerciseId))
            .ToListAsync();

        _context.Sets.RemoveRange(sets);
        _context.WorkoutExercises.RemoveRange(workoutExercises);

        var workout = await _context.Workouts.FindAsync(workoutId);

        if (workout != null)
        {
            _context.Workouts.Remove(workout);
        }

        await _context.SaveChangesAsync();
    }

    public async Task<List<Workout>> GetAllAsync(Guid userId)
    {
        return await _context.Workouts
            .Where(w => w.UserId == userId)
            .Include(w => w.WorkoutExercises)
                .ThenInclude(we => we.Exercise)
            .Include(w => w.WorkoutExercises)
                .ThenInclude(we => we.Sets)
            .AsNoTracking()
            .OrderByDescending(w => w.Date)
            .ToListAsync();
    }
}