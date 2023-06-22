using VitalCheckWeb.API.VitalCheck.Domain.Models;

namespace VitalCheckWeb.API.VitalCheck.Domain.Repositories;

public interface IMedicineTypeRepository
{
    Task<IEnumerable<MedicineType>> ListAsync();
    Task AddAsync(MedicineType medicineType);
    Task<MedicineType> FindByIdAsync(int medicineTypeId);
    void Update(MedicineType medicineType);
    void Remove(MedicineType medicineType);
}