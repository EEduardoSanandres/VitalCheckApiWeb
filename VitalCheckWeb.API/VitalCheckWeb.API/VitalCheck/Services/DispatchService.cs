using VitalCheckWeb.API.VitalCheck.Domain.Models;
using VitalCheckWeb.API.VitalCheck.Domain.Repositories;
using VitalCheckWeb.API.VitalCheck.Domain.Services;
using VitalCheckWeb.API.VitalCheck.Domain.Services.Communication;

namespace VitalCheckWeb.API.VitalCheck.Services;

public class DispatchService : IDispatchService
    {
        private readonly IDispatchRepository _dispatchRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IProviderRepository _providerRepository;
        private readonly IMedicineRepository _medicineRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DispatchService(IDispatchRepository dispatchRepository, ICompanyRepository companyRepository,
            IProviderRepository providerRepository, IMedicineRepository medicineRepository, IUnitOfWork unitOfWork)
        {
            _dispatchRepository = dispatchRepository;
            _companyRepository = companyRepository;
            _providerRepository = providerRepository;
            _medicineRepository = medicineRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Dispatch>> ListAsync()
        {
            return await _dispatchRepository.ListAsync();
        }

        public async Task<IEnumerable<Dispatch>> ListByCompanyIdAsync(int companyId)
        {
            return await _dispatchRepository.ListByCompanyIdAsync(companyId);
        }

        public async Task<IEnumerable<Dispatch>> ListByProviderIdAsync(int providerId)
        {
            return await _dispatchRepository.ListByProviderIdAsync(providerId);
        }

        public async Task<IEnumerable<Dispatch>> ListByMedicineIdAsync(int medicineId)
        {
            return await _dispatchRepository.ListByMedicineIdAsync(medicineId);
        }

        public async Task<DispatchResponse> SaveAsync(Dispatch dispatch)
        {
            try
            {
                var existingCompany = await _companyRepository.FindByIdAsync(dispatch.CompanyID);
                if (existingCompany == null)
                {
                    return new DispatchResponse("Company not found.");
                }

                var existingProvider = await _providerRepository.FindByIdAsync(dispatch.ProviderID);
                if (existingProvider == null)
                {
                    return new DispatchResponse("Provider not found.");
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