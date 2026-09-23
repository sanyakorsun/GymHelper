using GymHelper.Application.DTOs;
using GymHelper.Application.Interfaces;
using GymHelper.Domain.Entities;

namespace GymHelper.Application.Services;

public class ExerciseService
{
    private readonly IExerciseRepository _repository;

    public ExerciseService(IExerciseRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Exercise>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Exercise> CreateAsync(CreateExerciseDto dto)
    {
        var exercise = new Exercise
        {
            Name = dto.Name,
            MuscleGroup = dto.MuscleGroup,
            Description = dto.Description
        };

        return await _repository.AddAsync(exercise);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
    }
}