using Microsoft.EntityFrameworkCore;
using VitalCheckWeb.API.Shared.Persistence.Contexts;
using VitalCheckWeb.API.Shared.Persistence.Repositories;
using VitalCheckWeb.API.VitalCheck.Domain.Models;
using VitalCheckWeb.API.VitalCheck.Domain.Repositories;

namespace VitalCheckWeb.API.VitalCheck.Persistence.Repositories;

public class UserPlanRepository : BaseRepository, IUserPlanRepository
{
    public UserPlanRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<UserPlan>> ListAsync()
    {
        return await _context.UserPlans.ToListAsync();
    }

    public async Task AddAsync(UserPlan userPlan)
    {
        await _context.UserPlans.AddAsync(userPlan);
    }

    public async Task<UserPlan> FindByIdAsync(int userPlanId)
    {
        return await _context.UserPlans.FindAsync(userPlanId);
    }

    public void Update(UserPlan userPlan)
    {
        _context.UserPlans.Update(userPlan);
    }

    public void Remove(UserPlan userPlan)
    {
        _context.UserPlans.Remove(userPlan);
    }
}