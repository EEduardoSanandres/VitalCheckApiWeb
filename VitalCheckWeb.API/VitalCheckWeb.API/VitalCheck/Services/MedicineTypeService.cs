using VitalCheckWeb.API.VitalCheck.Domain.Models;
using VitalCheckWeb.API.VitalCheck.Domain.Repositories;
using VitalCheckWeb.API.VitalCheck.Domain.Services;
using VitalCheckWeb.API.VitalCheck.Domain.Services.Communication;

namespace VitalCheckWeb.API.VitalCheck.Services;

public class MedicineTypeService: IMedicineTypeService
{
    private readonly IMedicineTypeRepository _medicineTypeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public MedicineTypeService(IMedicineTypeRepository medicineTypeRepository, IUnitOfWork unitOfWork)
    {
        _medicineTypeRepository = medicineTypeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<MedicineType>> ListAsync()
    {
        return await _medicineTypeRepository.ListAsync();
    }

    public async Task<MedicineTypeResponse> SaveAsync(MedicineType medicineType)
    {
        try
        {
            await _medicineTypeRepository.AddAsync(medicineType);
            await _unitOfWork.CompleteAsync();
            return new MedicineTypeResponse(medicineType);
        }
        catch (Exception e)
        {
            return new MedicineTypeResponse($"An error occurred while saving the MedicineType: {e.Message}");
        }
    }

    public async Task<MedicineTypeResponse> UpdateAsync(int medicineTypeId, MedicineType medicineType)
    {
        var existingMedicineType = await _medicineTypeRepository.FindByIdAsync(medicineTypeId);
        if (existingMedicineType == null)
            return new MedicineTypeResponse("MedicineType not found.");

        existingMedicineType.TypeName = medicineType.TypeName;

        try
        {
            _medicineTypeRepository.Update(existingMedicineType);
            await _unitOfWork.CompleteAsync();
            return new MedicineTypeResponse(existingMedicineType);
        }
        catch (Exception e)
        {
            return new MedicineTypeResponse($"An error occurred while updating the MedicineType: {e.Message}");
        }
    }

    public async Task<MedicineTypeResponse> DeleteAsync(int medicineTypeId)
    {
        var existingMedicineType = await _medicineTypeRepository.FindByIdAsync(medicineTypeId);
        if (existingMedicineType == null)
            return new MedicineTypeResponse("MedicineType not found.");

        try
        {
            _medicineTypeRepository.Remove(existingMedicineType);
            await _unitOfWork.CompleteAsync();
            return new MedicineTypeResponse(existingMedicineType);
        }
        catch (Exception e)
        {
            return new MedicineTypeResponse($"An error occurred while deleting the MedicineType: {e.Message}");
        }
    }
}