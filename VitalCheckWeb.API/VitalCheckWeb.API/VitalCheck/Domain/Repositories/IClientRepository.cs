using VitalCheckWeb.API.VitalCheck.Domain.Models;

namespace VitalCheckWeb.API.VitalCheck.Domain.Repositories;

public interface IClientRepository
{
    Task<IEnumerable<Client>> ListAsync();
    Task AddAsync(Client client);
    Task<Client> FindByIdAsync(int clientId);
    void Update(Client client);
    void Remove(Client client);
}