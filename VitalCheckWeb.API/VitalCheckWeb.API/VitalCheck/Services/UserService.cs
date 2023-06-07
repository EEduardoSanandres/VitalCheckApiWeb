using VitalCheckWeb.API.VitalCheck.Domain.Models;
using VitalCheckWeb.API.VitalCheck.Domain.Repositories;
using VitalCheckWeb.API.VitalCheck.Domain.Services;
using VitalCheckWeb.API.VitalCheck.Domain.Services.Communication;

namespace VitalCheckWeb.API.VitalCheck.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    private readonly IUserPlanRepository _userPlanRepository;
    private readonly IUserTypeRepository _userTypeRepository;


    public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, IUserPlanRepository userPlanRepository, IUserTypeRepository userTypeRepository)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _userPlanRepository = userPlanRepository;
        _userTypeRepository = userTypeRepository;
    }

    public async Task<IEnumerable<User>> ListAsync()
    {
        return await _userRepository.ListAsync();
    }

    public async Task<IEnumerable<User>> ListByUserIdAsync(int userId)
    {
        return await _userRepository.ListByUserIdAsync(userId);
    }
    
    public async Task<IEnumerable<User>> ListByUserPlanIdAsync(int userPlanId)
    {
        return await _userRepository.ListByUserPlanIdAsync(userPlanId);
    }
    public async Task<UserResponse> SaveAsync(User user)
    {
        try
        {
            var userPlan = await _userPlanRepository.FindByIdAsync(user.UserPlanID);
            var userType = await _userTypeRepository.FindByIdAsync(user.UserTypeID);
            
            // Asignar el plan de usuario y tipo de usuario al usuario
            user.UserPlan = userPlan;
            user.UserType = userType;
            
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
        existingUser.UserTypeID = user.UserTypeID;

        try
        {
            _userRepository.Update(existingUser);
            await _unitOfWork.CompleteAsync();
            var updatedUser = await _userRepository.FindByIdAsync(userId); // Obtén la versión actualizada del usuario
            return new UserResponse(updatedUser);
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
            var deletedUser = existingUser; // Guarda una referencia al usuario eliminado
            _userRepository.Remove(existingUser);
            await _unitOfWork.CompleteAsync();
            return new UserResponse(deletedUser);
        }
        catch (Exception e)
        {
            return new UserResponse($"An error occurred while deleting the User: {e.Message}");
        }
    }
}