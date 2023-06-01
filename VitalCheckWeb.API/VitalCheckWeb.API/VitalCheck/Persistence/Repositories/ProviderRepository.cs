using Microsoft.EntityFrameworkCore;
using VitalCheckWeb.API.Shared.Persistence.Contexts;
using VitalCheckWeb.API.VitalCheck.Domain.Models;
using VitalCheckWeb.API.VitalCheck.Domain.Repositories;

namespace VitalCheckWeb.API.VitalCheck.Persistence.Repositories;

public class ProviderRepository : IProviderRepository
{
    private readonly AppDbContext _context;

    public ProviderRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Provider>> ListAsync()
    {
        return await _context.Providers
            .Include(p => p.User)
            .ThenInclude(u => u.UserPlan)
            .ToListAsync();
    }

    public async Task<IEnumerable<Provider>> ListByUserIdAsync(int userId)
    {
        return await _context.Providers
            .Where(p => p.UserID == userId)
            .Include(p => p.User)
            .ThenInclude(u => u.UserPlan)
            .ToListAsync();
    }

    public async Task AddAsync(Provider provider)
    {
        await _context.Providers.AddAsync(provider);
    }

    public async Task<Provider> FindByIdAsync(int providerId)
    {
        return await _context.Providers
            .Include(p => p.User)
            .ThenInclude(u => u.UserPlan)
            .FirstOrDefaultAsync(p => p.ProviderID == providerId );
    }

    public void Update(Provider provider)
    {
        _context.Providers.Update(provider);
    }

    public void Remove(Provider provider)
    {
        _context.Providers.Remove(provider);
    }
}