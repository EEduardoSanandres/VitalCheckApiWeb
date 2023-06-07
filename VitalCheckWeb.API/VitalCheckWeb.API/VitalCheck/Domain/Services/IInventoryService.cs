using VitalCheckWeb.API.VitalCheck.Domain.Models;
using VitalCheckWeb.API.VitalCheck.Domain.Services.Communication;

namespace VitalCheckWeb.API.VitalCheck.Domain.Services;

public interface IInventoryService
{
    Task<IEnumerable<Inventory>> ListAsync();
    Task<IEnumerable<Inventory>> ListByUserIdAsync(int userId);
    Task<IEnumerable<Inventory>> ListByMedicineIdAsync(int medicineId);
    Task<InventoryResponse> SaveAsync(Inventory inventory);
    Task<InventoryResponse> UpdateAsync(int inventoryId, Inventory inventory);
    Task<InventoryResponse> DeleteAsync(int inventoryId);
}