using VitalCheckWeb.API.VitalCheck.Domain.Models;
using VitalCheckWeb.API.VitalCheck.Domain.Services.Communication;

namespace VitalCheckWeb.API.VitalCheck.Domain.Services;

public interface IClientService
{
    Task<IEnumerable<Client>> ListAsync();
    Task<ClientResponse> SaveAsync(Client client);
    Task<ClientResponse> UpdateAsync(int clientId, Client client);
    Task<ClientResponse> DeleteAsync(int clientId);
}