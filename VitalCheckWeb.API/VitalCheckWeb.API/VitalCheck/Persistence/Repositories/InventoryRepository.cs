using Microsoft.EntityFrameworkCore;
using VitalCheckWeb.API.Shared.Persistence.Contexts;
using VitalCheckWeb.API.VitalCheck.Domain.Models;
using VitalCheckWeb.API.VitalCheck.Domain.Repositories;

namespace VitalCheckWeb.API.VitalCheck.Persistence.Repositories;

public class InventoryRepository : IInventoryRepository
{
    private readonly AppDbContext _context;

    public InventoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Inventory>> ListAsync()
    {
        return await _context.Inventories
            .Include(i => i.User)
            .ThenInclude(u => u.UserPlan)
            .Include(i => i.Medicine)
            .ThenInclude(m => m.MedicineType)
            .ToListAsync();
    }

    public async Task<IEnumerable<Inventory>> ListByUserIdAsync(int userId)
    {
        return await _context.Inventories
            .Include(i => i.User)
            .ThenInclude(u => u.UserPlan)
            .Include(i => i.Medicine)
            .ThenInclude(m => m.MedicineType)
            .Where(i => i.UserID == userId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Inventory>> ListByMedicineIdAsync(int medicineId)
    {
        return await _context.Inventories
            .Include(i => i.User)
            .ThenInclude(u => u.UserPlan)
            .Include(i => i.Medicine)
            .ThenInclude(m => m.MedicineType)
            .Where(i => i.MedicineID == medicineId)
            .ToListAsync();
    }

    public async Task AddAsync(Inventory inventory)
    {
        await _context.Inventories.AddAsync(inventory);
    }

    public async Task<Inventory> FindByIdAsync(int inventoryId)
    {
        return await _context.Inventories
            .Include(i => i.User)
            .ThenInclude(u => u.UserPlan)
            .Include(i => i.Medicine)
            .ThenInclude(m => m.MedicineType)
            .FirstOrDefaultAsync(i => i.InventoryID == inventoryId);
    }

    public void Update(Inventory inventory)
    {
        _context.Inventories.Update(inventory);
    }

    public void Remove(Inventory inventory)
    {
        _context.Inventories.Remove(inventory);
    }
}