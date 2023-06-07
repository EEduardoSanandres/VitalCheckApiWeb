using Microsoft.EntityFrameworkCore;
using VitalCheckWeb.API.Shared.Persistence.Contexts;
using VitalCheckWeb.API.Shared.Persistence.Repositories;
using VitalCheckWeb.API.VitalCheck.Domain.Models;
using VitalCheckWeb.API.VitalCheck.Domain.Repositories;

namespace VitalCheckWeb.API.VitalCheck.Persistence.Repositories;

public class ClientRepository : BaseRepository, IClientRepository
{
    public ClientRepository(AppDbContext context) : base(context)
    {
    }
    public async Task<IEnumerable<Client>> ListAsync()
    {
        return await _context.Clients.ToListAsync();
    }
    public async Task AddAsync(Client category)
    {
        await _context.Clients.AddAsync(category);
    }
    public async Task<Client> FindByIdAsync(int id)
    {
        return await _context.Clients.FindAsync(id);
    }
    public void Update(Client category)
    {
        _context.Clients.Update(category);
    }
    public void Remove(Client category)
    {
        _context.Clients.Remove(category);
    }
}