using VitalCheckWeb.API.VitalCheck.Domain.Models;
using VitalCheckWeb.API.VitalCheck.Domain.Services.Communication;

namespace VitalCheckWeb.API.VitalCheck.Domain.Services;

public interface IDispatchService
{
    Task<IEnumerable<Dispatch>> ListAsync();
    Task<IEnumerable<Dispatch>> ListByCompanyIdAsync(int companyId);
    Task<IEnumerable<Dispatch>> ListByProviderIdAsync(int providerId);
    Task<IEnumerable<Dispatch>> ListByMedicineIdAsync(int medicineId);
    Task<DispatchResponse> SaveAsync(Dispatch dispatch);
    Task<DispatchResponse> UpdateAsync(int dispatchId, Dispatch dispatch);
    Task<DispatchResponse> DeleteAsync(int dispatchId);
}