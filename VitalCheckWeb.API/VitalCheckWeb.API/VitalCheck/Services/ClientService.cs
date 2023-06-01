using VitalCheckWeb.API.VitalCheck.Domain.Models;
using VitalCheckWeb.API.VitalCheck.Domain.Repositories;
using VitalCheckWeb.API.VitalCheck.Domain.Services;
using VitalCheckWeb.API.VitalCheck.Domain.Services.Communication;

namespace VitalCheckWeb.API.VitalCheck.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;
    private readonly IUnitOfWork _unitOfWork;
    public ClientService(IClientRepository clientRepository, IUnitOfWork unitOfWork)
    {
        _clientRepository = clientRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<IEnumerable<Client>> ListAsync()
    {
        return await _clientRepository.ListAsync();
    }
    public async Task<ClientResponse> SaveAsync(Client client)
    {
        try
        {
            await _clientRepository.AddAsync(client);
            await _unitOfWork.CompleteAsync();
            return new ClientResponse(client);
        }
        catch (Exception e)
        {
            return new ClientResponse($"An error occurred while saving the Client: {e.Message}");
        }
    }
    public async Task<ClientResponse> UpdateAsync(int id, Client client)
    {
        var existingClient = await _clientRepository.FindByIdAsync(id);
        if (existingClient == null)
            return new ClientResponse("Client not found.");
        existingClient.DNI = client.DNI;
        existingClient.FirstName = client.FirstName;
        existingClient.LastName = client.LastName;
        try
        {
            _clientRepository.Update(existingClient);
            await _unitOfWork.CompleteAsync();
            return new ClientResponse(existingClient);
        }
        catch (Exception e)
        {
            return new ClientResponse($"An error occurred while updating the Client:{e.Message}");
        }
    }
    public async Task<ClientResponse> DeleteAsync(int id)
    {
        var existingCategory = await _clientRepository.FindByIdAsync(id);
        if (existingCategory == null)
            return new ClientResponse("Client not found.");
        try
        {
            _clientRepository.Remove(existingCategory);
            await _unitOfWork.CompleteAsync();
            return new ClientResponse(existingCategory);
        }
        catch (Exception e)
        {
            // Do some logging stuff
            return new ClientResponse($"An error occurred while deleting the Client:{e.Message}");
        }
    }
}