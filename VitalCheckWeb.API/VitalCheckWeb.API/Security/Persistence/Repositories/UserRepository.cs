﻿using Microsoft.EntityFrameworkCore;
using VitalCheckWeb.API.Security.Domain.Models;
using VitalCheckWeb.API.Shared.Persistence.Contexts;
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
            .Include(u => u.UserType)
            .ToListAsync();
    }
    
    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User> FindByIdAsync(int userId)
    {
        return await _context.Users
            .Include(u => u.UserPlan)
            .Include(u => u.UserType)
            .FirstOrDefaultAsync(u => u.UserID == userId);
    }

    public async Task<User> FindByUsernameAsync(string username)
    {
        return await _context.Users
            .Include(u => u.UserPlan)
            .Include(u => u.UserType)
            .FirstOrDefaultAsync(u => u.UserName == username);
    }

    public bool ExistsByUsername(string username)
    {
        return _context.Users.Any(x => x.UserName == username);
    }

    public User FindById(int id)
    {
        return _context.Users.Find(id);
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