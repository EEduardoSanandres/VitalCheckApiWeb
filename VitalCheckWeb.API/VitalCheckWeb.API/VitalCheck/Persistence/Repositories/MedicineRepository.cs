using Microsoft.EntityFrameworkCore;
using VitalCheckWeb.API.Shared.Persistence.Contexts;
using VitalCheckWeb.API.VitalCheck.Domain.Models;
using VitalCheckWeb.API.VitalCheck.Domain.Repositories;

namespace VitalCheckWeb.API.VitalCheck.Persistence.Repositories;

public class MedicineRepository : IMedicineRepository
{
    private readonly AppDbContext _context;

    public MedicineRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Medicine>> ListAsync()
    {
        return await _context.Medicines
            .Include(m => m.MedicineType)
            .ToListAsync();
    }

    public async Task<IEnumerable<Medicine>> ListByMedicineTypeIdAsync(int medicineTypeId)
    {
        return await _context.Medicines
            .Include(m => m.MedicineType)
            .Where(m => m.MedicineTypeID == medicineTypeId)
            .ToListAsync();
    }

    public async Task AddAsync(Medicine medicine)
    {
        await _context.Medicines.AddAsync(medicine);
    }

    public async Task<Medicine> FindByIdAsync(int medicineId)
    {
        return await _context.Medicines
            .Include(m => m.MedicineType)
            .FirstOrDefaultAsync(m => m.MedicineID == medicineId);
    }

    public void Update(Medicine medicine)
    {
        _context.Medicines.Update(medicine);
    }

    public void Remove(Medicine medicine)
    {
        _context.Medicines.Remove(medicine);
    }
}