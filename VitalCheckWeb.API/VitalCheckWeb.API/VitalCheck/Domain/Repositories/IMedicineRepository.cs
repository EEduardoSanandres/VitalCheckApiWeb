using VitalCheckWeb.API.VitalCheck.Domain.Models;

namespace VitalCheckWeb.API.VitalCheck.Domain.Repositories;

public interface IMedicineRepository
{
    Task<IEnumerable<Medicine>> ListAsync();
    Task AddAsync(Medicine medicine);
    Task<Medicine> FindByIdAsync(int medicineId);
    Task<IEnumerable<Medicine>> ListByMedicineTypeIdAsync(int medicineTypeId);
    void Update(Medicine medicine);
    void Remove(Medicine medicine);
}