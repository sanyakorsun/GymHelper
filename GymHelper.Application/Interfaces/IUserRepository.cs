using GymHelper.Domain.Entities;

namespace GymHelper.Application.Interfaces;

public interface IUserRepository
{
    Task<User> AddAsync(User user);
    Task<User> GetByIdAsync(Guid id);
    Task<User> UpdateAsync(User user);
}