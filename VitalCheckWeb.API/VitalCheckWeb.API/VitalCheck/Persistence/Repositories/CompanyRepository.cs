using Microsoft.EntityFrameworkCore;
using VitalCheckWeb.API.Shared.Persistence.Contexts;
using VitalCheckWeb.API.VitalCheck.Domain.Models;
using VitalCheckWeb.API.VitalCheck.Domain.Repositories;

namespace VitalCheckWeb.API.VitalCheck.Persistence.Repositories;

public class CompanyRepository : ICompanyRepository
{
    private readonly AppDbContext _context;

    public CompanyRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Company>> ListAsync()
    {
        return await _context.Companies
            .Include(c => c.User)
            .ThenInclude(u => u.UserPlan)
            .ToListAsync();
    }

    public async Task<IEnumerable<Company>> ListByUserIdAsync(int userId)
    {
        return await _context.Companies
            .Include(c => c.User)
            .ThenInclude(u => u.UserPlan)
            .Where(c => c.UserID == userId)
            .ToListAsync();
    }

    public async Task AddAsync(Company company)
    {
        await _context.Companies.AddAsync(company);
    }

    public async Task<Company> FindByIdAsync(int companyId)
    {
        return await _context.Companies
            .Include(c => c.User)
            .ThenInclude(u => u.UserPlan)
            .FirstOrDefaultAsync(c => c.CompanyID == companyId);
    }

    public void Update(Company company)
    {
        _context.Companies.Update(company);
    }

    public void Remove(Company company)
    {
        _context.Companies.Remove(company);
    }
}