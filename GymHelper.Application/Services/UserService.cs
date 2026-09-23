using GymHelper.Application.DTOs;
using GymHelper.Application.Interfaces;
using GymHelper.Domain.Entities;

namespace GymHelper.Application.Services;

public class UserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<User> CreateAsync(CreateUserDto dto)
    {
        var user = new User
        {
            Name = dto.Name,
            Age = dto.Age,
            Weight = dto.Weight,
            Height = dto.Height
        };

        return await _repository.AddAsync(user);
    }

    public async Task<User> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<User> UpdateAsync(Guid id, UpdateUserDto dto)
    {
        var user = await _repository.GetByIdAsync(id);

        if (user == null)
        {
            return null;
        }

        user.Name = dto.Name;
        user.Age = dto.Age;
        user.Weight = dto.Weight;
        user.Height = dto.Height;

        return await _repository.UpdateAsync(user);
    }
}