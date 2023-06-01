using VitalCheckWeb.API.VitalCheck.Domain.Models;
using VitalCheckWeb.API.VitalCheck.Domain.Repositories;
using VitalCheckWeb.API.VitalCheck.Domain.Services;
using VitalCheckWeb.API.VitalCheck.Domain.Services.Communication;

namespace VitalCheckWeb.API.VitalCheck.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<User>> ListAsync()
    {
        return await _userRepository.ListAsync();
    }

    public async Task<IEnumerable<User>> ListByUserPlanIdAsync(int userPlanId)
    {
        return await _userRepository.ListByUserPlanIdAsync(userPlanId);
    }
    public async Task<UserResponse> SaveAsync(User user)
    {
        try
        {
            await _userRepository.AddAsync(user);
            await _unitOfWork.CompleteAsync();
            return new UserResponse(user);
        }
        catch (Exception e)
        {
            return new UserResponse($"An error occurred while saving the User: {e.Message}");
        }
    }

    public async Task<UserResponse> UpdateAsync(int userId, User user)
    {
        var existingUser = await _userRepository.FindByIdAsync(userId);
        if (existingUser == null)
            return new UserResponse("User not found.");

        existingUser.UserName = user.UserName;
        existingUser.Email = user.Email;
        existingUser.Password = user.Password;
        existingUser.RUC = user.RUC;
        existingUser.RegistrationDate = user.RegistrationDate;
        existingUser.UserPlanID = user.UserPlanID;

        try
        {
            _userRepository.Update(existingUser);
            await _unitOfWork.CompleteAsync();
            return new UserResponse(existingUser);
        }
        catch (Exception e)
        {
            return new UserResponse($"An error occurred while updating the User: {e.Message}");
        }
    }

    public async Task<UserResponse> DeleteAsync(int userId)
    {
        var existingUser = await _userRepository.FindByIdAsync(userId);
        if (existingUser == null)
            return new UserResponse("User not found.");

        try
        {
            _userRepository.Remove(existingUser);
            await _unitOfWork.CompleteAsync();
            return new UserResponse(existingUser);
        }
        catch (Exception e)
        {
            return new UserResponse($"An error occurred while deleting the User: {e.Message}");
        }
    }
}