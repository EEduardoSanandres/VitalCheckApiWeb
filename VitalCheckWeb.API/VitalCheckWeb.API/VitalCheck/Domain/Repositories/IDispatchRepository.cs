using VitalCheckWeb.API.VitalCheck.Domain.Models;

namespace VitalCheckWeb.API.VitalCheck.Domain.Repositories;

public interface IDispatchRepository
{
    Task<IEnumerable<Dispatch>> ListAsync();
    Task<IEnumerable<Dispatch>> ListByCompanyIdAsync(int companyId);
    Task<IEnumerable<Dispatch>> ListByProviderIdAsync(int providerId);
    Task<IEnumerable<Dispatch>> ListByMedicineIdAsync(int medicineId);
    Task AddAsync(Dispatch dispatch);
    Task<Dispatch> FindByIdAsync(int dispatchId);
    void Update(Dispatch dispatch);
    void Remove(Dispatch dispatch);
}