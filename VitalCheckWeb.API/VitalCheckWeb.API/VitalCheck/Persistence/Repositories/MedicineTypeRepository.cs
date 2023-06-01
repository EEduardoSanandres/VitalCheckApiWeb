using Microsoft.EntityFrameworkCore;
using VitalCheckWeb.API.Shared.Persistence.Contexts;
using VitalCheckWeb.API.VitalCheck.Domain.Models;
using VitalCheckWeb.API.VitalCheck.Domain.Repositories;

namespace VitalCheckWeb.API.VitalCheck.Persistence.Repositories;

public class MedicineTypeRepository : IMedicineTypeRepository
{
    private readonly AppDbContext _context;

    public MedicineTypeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MedicineType>> ListAsync()
    {
        return await _context.MedicineTypes.ToListAsync();
    }

    public async Task AddAsync(MedicineType medicineType)
    {
        await _context.MedicineTypes.AddAsync(medicineType);
    }

    public async Task<MedicineType> FindByIdAsync(int medicineTypeId)
    {
        return await _context.MedicineTypes.FindAsync(medicineTypeId);
    }

    public void Update(MedicineType medicineType)
    {
        _context.MedicineTypes.Update(medicineType);
    }

    public void Remove(MedicineType medicineType)
    {
        _context.MedicineTypes.Remove(medicineType);
    }
}