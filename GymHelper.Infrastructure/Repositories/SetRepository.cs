using GymHelper.Application.Interfaces;
using GymHelper.Domain.Entities;
using GymHelper.Infrastructure.Data;

namespace GymHelper.Infrastructure.Repositories;

public class SetRepository : ISetRepository
{
    private readonly ApplicationDbContext _context;

    public SetRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Set> AddAsync(Set set)
    {
        _context.Sets.Add(set);
        await _context.SaveChangesAsync();

        return set;
    }
}