using GymHelper.Application.Interfaces;
using GymHelper.Domain.Entities;
using GymHelper.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GymHelper.Infrastructure.Repositories;

public class ExerciseRepository : IExerciseRepository
{
    private readonly ApplicationDbContext _context;

    public ExerciseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Exercise>> GetAllAsync()
    {
        return await _context.Exercises.ToListAsync();
    }

    public async Task<Exercise> AddAsync(Exercise exercise)
    {
        _context.Exercises.Add(exercise);
        await _context.SaveChangesAsync();

        return exercise;
    }
    public async Task DeleteAsync(Guid id)
    {
        var exercise = await _context.Exercises.FindAsync(id);

        if (exercise != null)
        {
            _context.Exercises.Remove(exercise);
            await _context.SaveChangesAsync();
        }
    }
}