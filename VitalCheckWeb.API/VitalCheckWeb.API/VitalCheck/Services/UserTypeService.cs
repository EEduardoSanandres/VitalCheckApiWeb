using VitalCheckWeb.API.VitalCheck.Domain.Models;
using VitalCheckWeb.API.VitalCheck.Domain.Repositories;
using VitalCheckWeb.API.VitalCheck.Domain.Services;
using VitalCheckWeb.API.VitalCheck.Domain.Services.Communication;

namespace VitalCheckWeb.API.VitalCheck.Services;

public class UserTypeService : IUserTypeService
{
    private readonly IUserTypeRepository _userTypeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UserTypeService(IUserTypeRepository userTypeRepository, IUnitOfWork unitOfWork)
    {
        _userTypeRepository = userTypeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<UserType>> ListAsync()
    {
        return await _userTypeRepository.ListAsync();
    }

    public async Task<UserTypeResponse> SaveAsync(UserType userType)
    {
        try
        {
            await _userTypeRepository.AddAsync(userType);
            await _unitOfWork.CompleteAsync();

            return new UserTypeResponse(userType);
        }
        catch (Exception ex)
        {
            // Manejo del error y registro, si es necesario
            return new UserTypeResponse($"An error occurred while saving the User Type: {ex.Message}");
        }
    }

    public async Task<UserTypeResponse> UpdateAsync(int userTypeId, UserType userType)
    {
        var existingUserType = await _userTypeRepository.FindByIdAsync(userTypeId);

        if (existingUserType == null)
            return new UserTypeResponse("User Type not found.");

        existingUserType.TypeName = userType.TypeName;

        try
        {
            _userTypeRepository.Update(existingUserType);
            await _unitOfWork.CompleteAsync();

            return new UserTypeResponse(existingUserType);
        }
        catch (Exception ex)
        {
            // Manejo del error y registro, si es necesario
            return new UserTypeResponse($"An error occurred while updating the User Plan: {ex.Message}");
        }
    }

    public async Task<UserTypeResponse> DeleteAsync(int userTypeId)
    {
        var existingUserType = await _userTypeRepository.FindByIdAsync(userTypeId);

        if (existingUserType == null)
            return new UserTypeResponse("User Type not found.");

        try
        {
            _userTypeRepository.Remove(existingUserType);
            await _unitOfWork.CompleteAsync();

            return new UserTypeResponse(existingUserType);
        }
        catch (Exception ex)
        {
            // Manejo del error y registro, si es necesario
            return new UserTypeResponse($"An error occurred while deleting the User Type: {ex.Message}");
        }
    }
}