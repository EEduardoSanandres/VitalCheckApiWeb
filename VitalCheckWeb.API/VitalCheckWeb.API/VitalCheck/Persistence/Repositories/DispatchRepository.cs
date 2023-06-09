﻿using Microsoft.EntityFrameworkCore;
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
                .Include(d => d.User1)
                .ThenInclude(u => u.UserPlan)
                .Include(d => d.User1)
                .ThenInclude(u => u.UserType)
                .Include(d => d.User2)
                .ThenInclude(u => u.UserPlan)
                .Include(d => d.User2)
                .ThenInclude(u => u.UserType)
                .Include(d => d.Medicine)
                .ThenInclude(m => m.MedicineType)
                .ToListAsync();
        }

        public async Task<IEnumerable<Dispatch>> ListByUser1IdAsync(int user1Id)
        {
            return await _context.Dispatches
                .Include(d => d.User1)
                .ThenInclude(u => u.UserPlan)
                .Include(d => d.User1)
                .ThenInclude(u => u.UserType)
                .Include(d => d.User2)
                .ThenInclude(u => u.UserPlan)
                .Include(d => d.User2)
                .ThenInclude(u => u.UserType)
                .Include(d => d.Medicine)
                .ThenInclude(m => m.MedicineType)
                .Where(d => d.User1ID == user1Id)
                .ToListAsync();
        }

        public async Task<IEnumerable<Dispatch>> ListByUser2IdAsync(int user2Id)
        {
            return await _context.Dispatches
                .Include(d => d.User1)
                .ThenInclude(u => u.UserPlan)
                .Include(d => d.User1)
                .ThenInclude(u => u.UserType)
                .Include(d => d.User2)
                .ThenInclude(u => u.UserPlan)
                .Include(d => d.User2)
                .ThenInclude(u => u.UserType)
                .Include(d => d.Medicine)
                .ThenInclude(m => m.MedicineType)
                .Where(d => d.User2ID == user2Id)
                .ToListAsync();
        }

        public async Task<IEnumerable<Dispatch>> ListByMedicineIdAsync(int medicineId)
        {
            return await _context.Dispatches
                .Include(d => d.User1)
                .ThenInclude(u => u.UserPlan)
                .Include(d => d.User1)
                .ThenInclude(u => u.UserType)
                .Include(d => d.User2)
                .ThenInclude(u => u.UserPlan)
                .Include(d => d.User2)
                .ThenInclude(u => u.UserType)
                .Include(d => d.Medicine)
                .ThenInclude(m => m.MedicineType)
                .Where(d => d.MedicineID == medicineId)
                .ToListAsync();
        }

        public async Task AddAsync(Dispatch dispatch)
        {
            await _context.Dispatches.AddAsync(dispatch);
            await _context.SaveChangesAsync();
        }

        public async Task<Dispatch> FindByIdAsync(int dispatchId)
        {
            return await _context.Dispatches
                .Include(d => d.User1)
                .ThenInclude(u => u.UserPlan)
                .Include(d => d.User1)
                .ThenInclude(u => u.UserType)
                .Include(d => d.User2)
                .ThenInclude(u => u.UserPlan)
                .Include(d => d.User2)
                .ThenInclude(u => u.UserType)
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