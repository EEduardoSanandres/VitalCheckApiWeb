using Microsoft.EntityFrameworkCore;
using VitalCheckWeb.API.Shared.Persistence.Contexts;
using VitalCheckWeb.API.VitalCheck.Domain.Models;
using VitalCheckWeb.API.VitalCheck.Domain.Repositories;

namespace VitalCheckWeb.API.VitalCheck.Persistence.Repositories;

public class DispatchRepository : IDispatchRepository
{
        private readonly AppDbContext _context;

        public DispatchRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Dispatch>> ListAsync()
        {
            return await _context.Dispatches
                .Include(d => d.Company)
                .ThenInclude(c => c.User)
                .ThenInclude(u => u.UserPlan)
                .Include(d => d.Provider)
                .ThenInclude(p => p.User)
                .ThenInclude(u => u.UserPlan)
                .Include(d => d.Medicine)
                .ThenInclude(m => m.MedicineType)
                .ToListAsync();
        }

        public async Task<IEnumerable<Dispatch>> ListByCompanyIdAsync(int companyId)
        {
            return await _context.Dispatches
                .Include(d => d.Company)
                .ThenInclude(c => c.User)
                .ThenInclude(u => u.UserPlan)
                .Include(d => d.Provider)
                .ThenInclude(p => p.User)
                .ThenInclude(u => u.UserPlan)
                .Include(d => d.Medicine)
                .ThenInclude(m => m.MedicineType)
                .Where(d => d.CompanyID == companyId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Dispatch>> ListByProviderIdAsync(int providerId)
        {
            return await _context.Dispatches
                .Include(d => d.Company)
                .ThenInclude(c => c.User)
                .ThenInclude(u => u.UserPlan)
                .Include(d => d.Provider)
                .ThenInclude(p => p.User)
                .ThenInclude(u => u.UserPlan)
                .Include(d => d.Medicine)
                .ThenInclude(m => m.MedicineType)
                .Where(d => d.ProviderID == providerId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Dispatch>> ListByMedicineIdAsync(int medicineId)
        {
            return await _context.Dispatches
                .Include(d => d.Company)
                .ThenInclude(c => c.User)
                .ThenInclude(u => u.UserPlan)
                .Include(d => d.Provider)
                .ThenInclude(p => p.User)
                .ThenInclude(u => u.UserPlan)
                .Include(d => d.Medicine)
                .ThenInclude(m => m.MedicineType)
                .Where(d => d.MedicineID == medicineId)
                .ToListAsync();
        }

        public async Task AddAsync(Dispatch dispatch)
        {
            await _context.Dispatches.AddAsync(dispatch);
        }

        public async Task<Dispatch> FindByIdAsync(int dispatchId)
        {
            return await _context.Dispatches
                .Include(d => d.Company)
                .ThenInclude(c => c.User)
                .ThenInclude(u => u.UserPlan)
                .Include(d => d.Provider)
                .ThenInclude(p => p.User)
                .ThenInclude(u => u.UserPlan)
                .Include(d => d.Medicine)
                .ThenInclude(m => m.MedicineType)
                .FirstOrDefaultAsync(d => d.DispatchID == dispatchId);
        }

        public void Update(Dispatch dispatch)
        {
            _context.Dispatches.Update(dispatch);
        }

        public void Remove(Dispatch dispatch)
        {
            _context.Dispatches.Remove(dispatch);
        }
}