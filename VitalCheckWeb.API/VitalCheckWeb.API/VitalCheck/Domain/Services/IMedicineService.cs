using VitalCheckWeb.API.VitalCheck.Domain.Models;
using VitalCheckWeb.API.VitalCheck.Domain.Services.Communication;

namespace VitalCheckWeb.API.VitalCheck.Domain.Services;

public interface IMedicineService
{
    Task<IEnumerable<Medicine>> ListAsync();
    Task<IEnumerable<Medicine>> ListByMedicineTypeIdAsync(int medicineTypeId);
    Task<MedicineResponse> SaveAsync(Medicine medicine);
    Task<MedicineResponse> UpdateAsync(int medicineId, Medicine medicine);
    Task<MedicineResponse> DeleteAsync(int medicineId);
}