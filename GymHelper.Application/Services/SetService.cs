using GymHelper.Application.DTOs;
using GymHelper.Application.Interfaces;
using GymHelper.Domain.Entities;

namespace GymHelper.Application.Services;

public class SetService
{
    private readonly ISetRepository _repository;

    public SetService(ISetRepository repository)
    {
        _repository = repository;
    }

    public async Task<Set> CreateAsync(CreateSetDto dto)
    {
        var set = new Set
        {
            WorkoutExerciseId = dto.WorkoutExerciseId,
            Weight = dto.Weight,
            Reps = dto.Reps
        };

        return await _repository.AddAsync(set);
    }
}