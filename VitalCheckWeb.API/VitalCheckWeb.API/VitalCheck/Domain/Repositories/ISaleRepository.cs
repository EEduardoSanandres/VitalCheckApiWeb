using VitalCheckWeb.API.VitalCheck.Domain.Models;

namespace VitalCheckWeb.API.VitalCheck.Domain.Repositories;

public interface ISaleRepository
{
    Task<IEnumerable<Sale>> ListAsync();
    Task<IEnumerable<Sale>> ListByUserIdAsync(int userId);
    Task<IEnumerable<Sale>> ListByClientIdAsync(int clientId);
    Task<IEnumerable<Sale>> ListByMedicineIdAsync(int medicineId);
    Task AddAsync(Sale sale);
    Task<Sale> FindByIdAsync(int saleId);
    void Update(Sale sale);
    void Remove(Sale sale);
}