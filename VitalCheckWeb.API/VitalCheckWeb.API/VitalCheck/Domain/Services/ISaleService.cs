using VitalCheckWeb.API.VitalCheck.Domain.Models;
using VitalCheckWeb.API.VitalCheck.Domain.Services.Communication;

namespace VitalCheckWeb.API.VitalCheck.Domain.Services;

public interface ISaleService
{
    Task<IEnumerable<Sale>> ListAsync();
    Task<IEnumerable<Sale>> ListByCompanyIdAsync(int companyId);
    Task<IEnumerable<Sale>> ListByClientIdAsync(int clientId);
    Task<IEnumerable<Sale>> ListByMedicineIdAsync(int medicineId);
    Task<SaleResponse> SaveAsync(Sale sale);
    Task<SaleResponse> UpdateAsync(int saleId, Sale sale);
    Task<SaleResponse> DeleteAsync(int saleId);
}