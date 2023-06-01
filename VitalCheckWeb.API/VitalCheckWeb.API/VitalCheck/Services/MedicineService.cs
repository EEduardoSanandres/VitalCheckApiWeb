using VitalCheckWeb.API.VitalCheck.Domain.Models;
using VitalCheckWeb.API.VitalCheck.Domain.Repositories;
using VitalCheckWeb.API.VitalCheck.Domain.Services;
using VitalCheckWeb.API.VitalCheck.Domain.Services.Communication;

namespace VitalCheckWeb.API.VitalCheck.Services;

public class MedicineService : IMedicineService
{
    private readonly IMedicineRepository _medicineRepository;
    private readonly IUnitOfWork _unitOfWork;

    public MedicineService(IMedicineRepository medicineRepository, IUnitOfWork unitOfWork)
    {
        _medicineRepository = medicineRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Medicine>> ListAsync()
    {
        return await _medicineRepository.ListAsync();
    }

    public async Task<IEnumerable<Medicine>> ListByMedicineTypeIdAsync(int medicineTypeId)
    {
        return await _medicineRepository.ListByMedicineTypeIdAsync(medicineTypeId);
    }

    public async Task<MedicineResponse> SaveAsync(Medicine medicine)
    {
        try
        {
            await _medicineRepository.AddAsync(medicine);
            await _unitOfWork.CompleteAsync();
            return new MedicineResponse(medicine);
        }
        catch (Exception e)
        {
            return new MedicineResponse($"An error occurred while saving the Medicine: {e.Message}");
        }
    }

    public async Task<MedicineResponse> UpdateAsync(int medicineId, Medicine medicine)
    {
        var existingMedicine = await _medicineRepository.FindByIdAsync(medicineId);
        if (existingMedicine == null)
            return new MedicineResponse("Medicine not found.");

        existingMedicine.CommercialName = medicine.CommercialName;
        existingMedicine.GenericName = medicine.GenericName;
        existingMedicine.CostPrice = medicine.CostPrice;

        try
        {
            _medicineRepository.Update(existingMedicine);
            await _unitOfWork.CompleteAsync();
            return new MedicineResponse(existingMedicine);
        }
        catch (Exception e)
        {
            return new MedicineResponse($"An error occurred while updating the Medicine: {e.Message}");
        }
    }

    public async Task<MedicineResponse> DeleteAsync(int medicineId)
    {
        var existingMedicine = await _medicineRepository.FindByIdAsync(medicineId);
        if (existingMedicine == null)
            return new MedicineResponse("Medicine not found.");

        try
        {
            _medicineRepository.Remove(existingMedicine);
            await _unitOfWork.CompleteAsync();
            return new MedicineResponse(existingMedicine);
        }
        catch (Exception e)
        {
            return new MedicineResponse($"An error occurred while deleting the Medicine: {e.Message}");
        }
    }
}