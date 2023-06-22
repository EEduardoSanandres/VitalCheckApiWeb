using Microsoft.EntityFrameworkCore;
using VitalCheckWeb.API.Shared.Persistence.Contexts;
using VitalCheckWeb.API.Shared.Persistence.Repositories;
using VitalCheckWeb.API.VitalCheck.Domain.Models;
using VitalCheckWeb.API.VitalCheck.Domain.Repositories;

namespace VitalCheckWeb.API.VitalCheck.Persistence.Repositories;

public class UserTypeRepository : BaseRepository, IUserTypeRepository
{
    public UserTypeRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<UserType>> ListAsync()
    {
        return await _context.UserTypes.ToListAsync();
    }

    public async Task AddAsync(UserType userType)
    {
        await _context.UserTypes.AddAsync(userType);
    }

    public async Task<UserType> FindByIdAsync(int userTypeId)
    {
        return await _context.UserTypes.FindAsync(userTypeId);
    }

    public void Update(UserType userType)
    {
        _context.UserTypes.Update(userType);
    }

    public void Remove(UserType userType)
    {
        _context.UserTypes.Remove(userType);
    }
}