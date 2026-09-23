using GymHelper.Application.DTOs;
using GymHelper.Application.Interfaces;
using GymHelper.Domain.Entities;

namespace GymHelper.Application.Services;

public class WorkoutService
{
    private readonly IWorkoutRepository _repository;

    public WorkoutService(IWorkoutRepository repository)
    {
        _repository = repository;
    }

    public async Task<Workout> CreateAsync(CreateWorkoutDto dto)
    {
        var workout = new Workout
        {
            UserId = dto.UserId,
            Date = dto.Date
        };

        return await _repository.AddAsync(workout);
    }

    public async Task<bool> FinishAsync(Guid workoutId)
    {
        var hasSets = await _repository.HasSetsAsync(workoutId);

        if (!hasSets)
        {
            await _repository.DeleteAsync(workoutId);
            return false;
        }

        return true;
    }

    public async Task<List<Workout>> GetAllAsync(Guid userId)
    {
        return await _repository.GetAllAsync(userId);
    }

    public async Task DeleteAsync(Guid workoutId)
    {
        await _repository.DeleteAsync(workoutId);
    }
}