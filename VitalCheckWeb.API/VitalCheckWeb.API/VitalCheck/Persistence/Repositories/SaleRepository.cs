using Microsoft.EntityFrameworkCore;
using VitalCheckWeb.API.Shared.Persistence.Contexts;
using VitalCheckWeb.API.VitalCheck.Domain.Models;
using VitalCheckWeb.API.VitalCheck.Domain.Repositories;

namespace VitalCheckWeb.API.VitalCheck.Persistence.Repositories;

public class SaleRepository : ISaleRepository
{
    private readonly AppDbContext _context;

    public SaleRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Sale>> ListAsync()
    {
        return await _context.Sales
            .Include(s => s.Company)
            .ThenInclude(c => c.User)
            .ThenInclude(u => u.UserPlan)
            .Include(s => s.Client)
            .Include(s => s.Medicine)
            .ThenInclude(m => m.MedicineType)
            .ToListAsync();
    }

    public async Task<IEnumerable<Sale>> ListByCompanyIdAsync(int companyId)
    {
        return await _context.Sales
            .Include(s => s.Company)
            .ThenInclude(c => c.User)
            .ThenInclude(u => u.UserPlan)
            .Include(s => s.Client)
            .Include(s => s.Medicine)
            .ThenInclude(m => m.MedicineType)
            .Where(s => s.CompanyID == companyId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Sale>> ListByClientIdAsync(int clientId)
    {
        return await _context.Sales
            .Include(s => s.Company)
            .ThenInclude(c => c.User)
            .ThenInclude(u => u.UserPlan)
            .Include(s => s.Client)
            .Include(s => s.Medicine)
            .ThenInclude(m => m.MedicineType)
            .Where(s => s.ClientID == clientId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Sale>> ListByMedicineIdAsync(int medicineId)
    {
        return await _context.Sales
            .Include(s => s.Company)
            .ThenInclude(c => c.User)
            .ThenInclude(u => u.UserPlan)
            .Include(s => s.Client)
            .Include(s => s.Medicine)
            .ThenInclude(m => m.MedicineType)
            .Where(s => s.MedicineID == medicineId)
            .ToListAsync();
    }

    public async Task AddAsync(Sale sale)
    {
        await _context.Sales.AddAsync(sale);
    }

    public async Task<Sale> FindByIdAsync(int saleId)
    {
        return await _context.Sales
            .Include(s => s.Company)
            .ThenInclude(c => c.User)
            .ThenInclude(u => u.UserPlan)
            .Include(s => s.Client)
            .Include(s => s.Medicine)
            .ThenInclude(m => m.MedicineType)
            .FirstOrDefaultAsync(s => s.SaleID == saleId);
    }

    public void Update(Sale sale)
    {
        _context.Sales.Update(sale);
    }

    public void Remove(Sale sale)
    { 
        _context.Sales.Remove(sale);
    }
}