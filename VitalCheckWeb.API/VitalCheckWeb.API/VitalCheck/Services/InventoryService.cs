using VitalCheckWeb.API.VitalCheck.Domain.Models;
using VitalCheckWeb.API.VitalCheck.Domain.Repositories;
using VitalCheckWeb.API.VitalCheck.Domain.Services;
using VitalCheckWeb.API.VitalCheck.Domain.Services.Communication;

namespace VitalCheckWeb.API.VitalCheck.Services;

public class InventoryService : IInventoryService
{
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMedicineRepository _medicineRepository;
        private readonly IUnitOfWork _unitOfWork;

        public InventoryService(IInventoryRepository inventoryRepository, IUserRepository userRepository, IMedicineRepository medicineRepository, IUnitOfWork unitOfWork)
        {
            _inventoryRepository = inventoryRepository;
            _userRepository = userRepository;
            _medicineRepository = medicineRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Inventory>> ListAsync()
        {
            return await _inventoryRepository.ListAsync();
        }

        public async Task<IEnumerable<Inventory>> ListByUserIdAsync(int userId)
        {
            return await _inventoryRepository.ListByUserIdAsync(userId);
        }

        public async Task<IEnumerable<Inventory>> ListByMedicineIdAsync(int medicineId)
        {
            return await _inventoryRepository.ListByMedicineIdAsync(medicineId);
        }

        public async Task<InventoryResponse> SaveAsync(Inventory inventory)
        {
            try
            {
                var existingUser = await _userRepository.FindByIdAsync(inventory.UserID);
                if (existingUser == null)
                {
                    return new InventoryResponse("User not found.");
                }

                var existingMedicine = await _medicineRepository.FindByIdAsync(inventory.MedicineID);
                if (existingMedicine == null)
                {
                    return new InventoryResponse("Medicine not found.");
                }

                await _inventoryRepository.AddAsync(inventory);
                await _unitOfWork.CompleteAsync();
                return new InventoryResponse(inventory);
            }
            catch (Exception e)
            {
                return new InventoryResponse($"An error occurred while saving the Inventory: {e.Message}");
            }
        }

        public async Task<InventoryResponse> UpdateAsync(int inventoryId, Inventory inventory)
        {
            var existingInventory = await _inventoryRepository.FindByIdAsync(inventoryId);
            if (existingInventory == null)
            {
                return new InventoryResponse("Inventory not found.");
            }

            existingInventory.Quantity = inventory.Quantity;
            existingInventory.SalePrice = inventory.SalePrice;

            try
            {
                _inventoryRepository.Update(existingInventory);
                await _unitOfWork.CompleteAsync();
                return new InventoryResponse(existingInventory);
            }
            catch (Exception e)
            {
                return new InventoryResponse($"An error occurred while updating the Inventory: {e.Message}");
            }
        }

        public async Task<InventoryResponse> DeleteAsync(int inventoryId)
        {
            var existingInventory = await _inventoryRepository.FindByIdAsync(inventoryId);
            if (existingInventory == null)
            {
                return new InventoryResponse("Inventory not found.");
            }

            try
            {
                _inventoryRepository.Remove(existingInventory);
                await _unitOfWork.CompleteAsync();
                return new InventoryResponse(existingInventory);
            }
            catch (Exception e)
            {
                return new InventoryResponse($"An error occurred while deleting the Inventory: {e.Message}");
            }
        }
}