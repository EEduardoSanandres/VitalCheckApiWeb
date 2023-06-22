using VitalCheckWeb.API.VitalCheck.Domain.Models;
using VitalCheckWeb.API.VitalCheck.Domain.Services.Communication;

namespace VitalCheckWeb.API.VitalCheck.Domain.Services;

public interface IMedicineTypeService
{
    Task<IEnumerable<MedicineType>> ListAsync();
    Task<MedicineTypeResponse> SaveAsync(MedicineType medicineType);
    Task<MedicineTypeResponse> UpdateAsync(int medicineTypeId, MedicineType medicineType);
    Task<MedicineTypeResponse> DeleteAsync(int medicineTypeId);
}