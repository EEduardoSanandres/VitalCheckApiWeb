using VitalCheckWeb.API.VitalCheck.Domain.Models;

namespace VitalCheckWeb.API.VitalCheck.Domain.Repositories;

public interface IProviderRepository
{
    Task<IEnumerable<Provider>> ListAsync();
    Task<IEnumerable<Provider>> ListByUserIdAsync(int userId);
    Task AddAsync(Provider provider);
    Task<Provider> FindByIdAsync(int providerId);
    void Update(Provider provider);
    void Remove(Provider provider);
}