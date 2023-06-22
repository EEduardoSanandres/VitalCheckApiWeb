using VitalCheckWeb.API.VitalCheck.Domain.Models;
using VitalCheckWeb.API.VitalCheck.Domain.Repositories;
using VitalCheckWeb.API.VitalCheck.Domain.Services;
using VitalCheckWeb.API.VitalCheck.Domain.Services.Communication;

namespace VitalCheckWeb.API.VitalCheck.Services;

public class SaleService : ISaleService
{
    private readonly ISaleRepository _saleRepository;
    private readonly IUserRepository _userRepository;
    private readonly IClientRepository _clientRepository;
    private readonly IMedicineRepository _medicineRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SaleService(
        ISaleRepository saleRepository,
        IUserRepository userRepository,
        IClientRepository clientRepository,
        IMedicineRepository medicineRepository,
        IUnitOfWork unitOfWork)
    {
        _saleRepository = saleRepository;
        _userRepository = userRepository;
        _clientRepository = clientRepository;
        _medicineRepository = medicineRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Sale>> ListAsync()
    {
        return await _saleRepository.ListAsync();
    }

    public async Task<IEnumerable<Sale>> ListByUserIdAsync(int userId)
    {
        return await _saleRepository.ListByUserIdAsync(userId);
    }

    public async Task<IEnumerable<Sale>> ListByClientIdAsync(int clientId)
    {
        return await _saleRepository.ListByClientIdAsync(clientId);
    }

    public async Task<IEnumerable<Sale>> ListByMedicineIdAsync(int medicineId)
    {
        return await _saleRepository.ListByMedicineIdAsync(medicineId);
    }

    public async Task<SaleResponse> SaveAsync(Sale sale)
    {
        try
        {
            var existingCompany = await _userRepository.FindByIdAsync(sale.UserID);
            if (existingCompany == null)
            {
                return new SaleResponse("User not found.");
            }

            var existingClient = await _clientRepository.FindByIdAsync(sale.ClientID);
            if (existingClient == null)
            {
                return new SaleResponse("Client not found.");
            }

            var existingMedicine = await _medicineRepository.FindByIdAsync(sale.MedicineID);
            if (existingMedicine == null)
            {
                return new SaleResponse("Medicine not found.");
            }

            await _saleRepository.AddAsync(sale);
            await _unitOfWork.CompleteAsync();
            return new SaleResponse(sale);
        }
        catch (Exception e)
        {
            return new SaleResponse($"An error occurred while saving the Sale: {e.Message}");
        }
    }

    public async Task<SaleResponse> UpdateAsync(int saleId, Sale sale)
    {
        var existingSale = await _saleRepository.FindByIdAsync(saleId);
        if (existingSale == null)
        {
            return new SaleResponse("Sale not found.");
        }

        existingSale.Quantity = sale.Quantity;
        existingSale.TotalPrice = sale.TotalPrice;
        existingSale.Date = sale.Date;
        existingSale.UserID = sale.UserID;
        existingSale.ClientID = sale.ClientID;
        existingSale.MedicineID = sale.MedicineID;

        try
        {
            _saleRepository.Update(existingSale);
            await _unitOfWork.CompleteAsync();
            return new SaleResponse(existingSale);
        }
        catch (Exception e)
        {
            return new SaleResponse($"An error occurred while updating the Sale: {e.Message}");
        }
    }

    public async Task<SaleResponse> DeleteAsync(int saleId)
    {
        var existingSale = await _saleRepository.FindByIdAsync(saleId);
        if (existingSale == null)
        {
            return new SaleResponse("Sale not found.");
        }

        try
        {
            _saleRepository.Remove(existingSale);
            await _unitOfWork.CompleteAsync();
            return new SaleResponse(existingSale);
        }
        catch (Exception e)
        {
            return new SaleResponse($"An error occurred while deleting the Sale: {e.Message}");
        } 
    }
}