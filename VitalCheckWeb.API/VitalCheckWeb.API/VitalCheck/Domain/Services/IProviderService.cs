using VitalCheckWeb.API.VitalCheck.Domain.Models;
using VitalCheckWeb.API.VitalCheck.Domain.Services.Communication;

namespace VitalCheckWeb.API.VitalCheck.Domain.Services;

public interface IProviderService
{
    Task<IEnumerable<Provider>> ListAsync();
    Task<IEnumerable<Provider>> ListByUserIdAsync(int userId);
    Task<ProviderResponse> SaveAsync(Provider provider);
    Task<ProviderResponse> UpdateAsync(int providerId, Provider provider);
    Task<ProviderResponse> DeleteAsync(int providerId);
}