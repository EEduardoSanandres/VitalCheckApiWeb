using VitalCheckWeb.API.VitalCheck.Domain.Models;

namespace VitalCheckWeb.API.VitalCheck.Domain.Repositories;

public interface IInventoryRepository
{
    Task<IEnumerable<Inventory>> ListAsync();
    Task<IEnumerable<Inventory>> ListByUserIdAsync(int userId);
    Task<IEnumerable<Inventory>> ListByMedicineIdAsync(int medicineId);
    Task AddAsync(Inventory inventory);
    Task<Inventory> FindByIdAsync(int inventoryId);
    void Update(Inventory inventory);
    void Remove(Inventory inventory);
}