using GymHelper.Domain.Entities;

namespace GymHelper.Application.Interfaces;

public interface ISetRepository
{
    Task<Set> AddAsync(Set set);
}