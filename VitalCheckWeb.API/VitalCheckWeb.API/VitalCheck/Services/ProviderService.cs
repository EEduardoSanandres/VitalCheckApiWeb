using VitalCheckWeb.API.VitalCheck.Domain.Models;
using VitalCheckWeb.API.VitalCheck.Domain.Repositories;
using VitalCheckWeb.API.VitalCheck.Domain.Services;
using VitalCheckWeb.API.VitalCheck.Domain.Services.Communication;

namespace VitalCheckWeb.API.VitalCheck.Services;

public class ProviderService : IProviderService
{
    private readonly IProviderRepository _providerRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ProviderService(IProviderRepository providerRepository, IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _providerRepository = providerRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Provider>> ListAsync()
    {
        return await _providerRepository.ListAsync();
    }

    public async Task<IEnumerable<Provider>> ListByUserIdAsync(int userId)
    {
        return await _providerRepository.ListByUserIdAsync(userId);
    }

    public async Task<ProviderResponse> SaveAsync(Provider provider)
    {
        try
        {
            var existingUser = await _userRepository.FindByIdAsync(provider.UserID);
            if (existingUser == null)
            {
                return new ProviderResponse("User not found.");
            }

            await _providerRepository.AddAsync(provider);
            await _unitOfWork.CompleteAsync();
            return new ProviderResponse(provider);
        }
        catch (Exception e)
        {
            return new ProviderResponse($"An error occurred while saving the Provider: {e.Message}");
        }
    }

    public async Task<ProviderResponse> UpdateAsync(int providerId, Provider provider)
    {
        var existingProvider = await _providerRepository.FindByIdAsync(providerId);
        if (existingProvider == null)
        {
            return new ProviderResponse("Provider not found.");
        }

        existingProvider.UserID = provider.UserID;

        try
        {
            _providerRepository.Update(existingProvider);
            await _unitOfWork.CompleteAsync();
            return new ProviderResponse(existingProvider);
        }
        catch (Exception e)
        {
            return new ProviderResponse($"An error occurred while updating the Provider: {e.Message}");
        }
    }

    public async Task<ProviderResponse> DeleteAsync(int providerId)
    {
        var existingProvider = await _providerRepository.FindByIdAsync(providerId);
        if (existingProvider == null)
        {
            return new ProviderResponse("Provider not found.");
        }

        try
        {
            _providerRepository.Remove(existingProvider);
            await _unitOfWork.CompleteAsync();
            return new ProviderResponse(existingProvider);
        }
        catch (Exception e)
        {
            return new ProviderResponse($"An error occurred while deleting the Provider: {e.Message}");
        }
    }
}