using Microsoft.EntityFrameworkCore;
using VitalCheckWeb.API.Shared.Persistence.Contexts;
using VitalCheckWeb.API.VitalCheck.Domain.Models;
using VitalCheckWeb.API.VitalCheck.Domain.Repositories;

namespace VitalCheckWeb.API.VitalCheck.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> ListAsync()
    {
        return await _context.Users
            .Include(u => u.UserPlan)
            .ToListAsync();
    }

    public async Task<IEnumerable<User>> ListByUserPlanIdAsync(int userPlanId)
    {
        return await _context.Users
            .Include(u => u.UserPlan)
            .Where(u => u.UserPlanID == userPlanId)
            .ToListAsync();
    }
    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public async Task<User> FindByIdAsync(int userId)
    {
        return await _context.Users
            .Include(u => u.UserPlan)
            .FirstOrDefaultAsync(u => u.UserID == userId);
    }

    public void Update(User user)
    {
        _context.Users.Update(user);
    }

    public void Remove(User user)
    {
        _context.Users.Remove(user);
    }
}