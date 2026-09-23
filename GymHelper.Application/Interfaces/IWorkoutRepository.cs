using GymHelper.Domain.Entities;

namespace GymHelper.Application.Interfaces;

public interface IWorkoutRepository
{
    Task<Workout> AddAsync(Workout workout);
    Task<bool> HasSetsAsync(Guid workoutId);
    Task DeleteAsync(Guid workoutId);
    Task<List<Workout>> GetAllAsync(Guid userId);
}