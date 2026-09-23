using GymHelper.Domain.Entities;

namespace GymHelper.Application.Interfaces;

public interface IExerciseRepository
{
    Task<Exercise> AddAsync(Exercise exercise);
    Task<List<Exercise>> GetAllAsync();
    Task DeleteAsync(Guid id);
}