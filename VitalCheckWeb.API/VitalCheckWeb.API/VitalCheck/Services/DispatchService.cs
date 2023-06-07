using VitalCheckWeb.API.VitalCheck.Domain.Models;
using VitalCheckWeb.API.VitalCheck.Domain.Repositories;
using VitalCheckWeb.API.VitalCheck.Domain.Services;
using VitalCheckWeb.API.VitalCheck.Domain.Services.Communication;

namespace VitalCheckWeb.API.VitalCheck.Services;

public class DispatchService : IDispatchService
    {
        private readonly IDispatchRepository _dispatchRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMedicineRepository _medicineRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DispatchService(IDispatchRepository dispatchRepository, IUserRepository userRepository, IMedicineRepository medicineRepository, IUnitOfWork unitOfWork)
        {
            _dispatchRepository = dispatchRepository;
            _userRepository = userRepository;
            _medicineRepository = medicineRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Dispatch>> ListAsync()
        {
            return await _dispatchRepository.ListAsync();
        }

        public async Task<IEnumerable<Dispatch>> ListByUser1IdAsync(int user1Id)
        {
            return await _dispatchRepository.ListByUser1IdAsync(user1Id);
        }

        public async Task<IEnumerable<Dispatch>> ListByUser2IdAsync(int user2Id)
        {
            return await _dispatchRepository.ListByUser2IdAsync(user2Id);
        }

        public async Task<IEnumerable<Dispatch>> ListByMedicineIdAsync(int medicineId)
        {
            return await _dispatchRepository.ListByMedicineIdAsync(medicineId);
        }

        public async Task<DispatchResponse> SaveAsync(Dispatch dispatch)
        {
            try
            {
                var existingCompany = await _userRepository.FindByIdAsync(dispatch.User1ID);
                if (existingCompany == null)
                {
                    return new DispatchResponse("User 1 not found.");
                }

                var existingProvider = await _userRepository.FindByIdAsync(dispatch.User2ID);
                if (existingProvider == null)
                {
                    return new DispatchResponse("User 2 not found.");
                }

                var existingMedicine = await _medicineRepository.FindByIdAsync(dispatch.MedicineID);
                if (existingMedicine == null)
                {
                    return new DispatchResponse("Medicine not found.");
                }

                dispatch.EntryDate = DateTime.Now; // Set the current date/time

                await _dispatchRepository.AddAsync(dispatch);
                await _unitOfWork.CompleteAsync();

                return new DispatchResponse(dispatch);
            }
            catch (Exception e)
            {
                return new DispatchResponse($"An error occurred while saving the Dispatch: {e.Message}");
            }
        }

        public async Task<DispatchResponse> UpdateAsync(int dispatchId, Dispatch dispatch)
        {
            var existingDispatch = await _dispatchRepository.FindByIdAsync(dispatchId);
            if (existingDispatch == null)
            {
                return new DispatchResponse("Dispatch not found.");
            }

            existingDispatch.Quantity = dispatch.Quantity;
            existingDispatch.Description = dispatch.Description;
            existingDispatch.EntryDate = dispatch.EntryDate;
            existingDispatch.ExpiryDate = dispatch.ExpiryDate;
            existingDispatch.User1ID = dispatch.User1ID;
            existingDispatch.User2ID = dispatch.User2ID;
            existingDispatch.MedicineID = dispatch.MedicineID;
            
            try
            {
                _dispatchRepository.Update(existingDispatch);
                await _unitOfWork.CompleteAsync();
                return new DispatchResponse(existingDispatch);
            }
            catch (Exception e)
            {
                return new DispatchResponse($"An error occurred while updating the Dispatch: {e.Message}");
            }
        }

        public async Task<DispatchResponse> DeleteAsync(int dispatchId)
        {
            var existingDispatch = await _dispatchRepository.FindByIdAsync(dispatchId);
            if (existingDispatch == null)
            {
                return new DispatchResponse("Dispatch not found.");
            }

            try
            {
                _dispatchRepository.Remove(existingDispatch);
                await _unitOfWork.CompleteAsync();
                return new DispatchResponse(existingDispatch);
            }
            catch (Exception e)
            {
                return new DispatchResponse($"An error occurred while deleting the Dispatch: {e.Message}");
            }
        }
    }