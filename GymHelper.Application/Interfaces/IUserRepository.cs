using GymHelper.Domain.Entities;

namespace GymHelper.Application.Interfaces;

public interface IUserRepository
{
    Task<User> AddAsync(User user);
}