using VitalCheckWeb.API.VitalCheck.Domain.Models;
using VitalCheckWeb.API.VitalCheck.Domain.Repositories;
using VitalCheckWeb.API.VitalCheck.Domain.Services;
using VitalCheckWeb.API.VitalCheck.Domain.Services.Communication;

namespace VitalCheckWeb.API.VitalCheck.Services;

public class UserPlanService : IUserPlanService
{
    private readonly IUserPlanRepository _userPlanRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UserPlanService(IUserPlanRepository userPlanRepository, IUnitOfWork unitOfWork)
    {
        _userPlanRepository = userPlanRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<UserPlan>> ListAsync()
    {
        return await _userPlanRepository.ListAsync();
    }

    public async Task<UserPlanResponse> SaveAsync(UserPlan userPlan)
    {
        try
        {
            await _userPlanRepository.AddAsync(userPlan);
            await _unitOfWork.CompleteAsync();

            return new UserPlanResponse(userPlan);
        }
        catch (Exception ex)
        {
            // Manejo del error y registro, si es necesario
            return new UserPlanResponse($"An error occurred while saving the User Plan: {ex.Message}");
        }
    }

    public async Task<UserPlanResponse> UpdateAsync(int userPlanId, UserPlan userPlan)
    {
        var existingUserPlan = await _userPlanRepository.FindByIdAsync(userPlanId);

        if (existingUserPlan == null)
            return new UserPlanResponse("User Plan not found.");

        existingUserPlan.PlanName = userPlan.PlanName;

        try
        {
            _userPlanRepository.Update(existingUserPlan);
            await _unitOfWork.CompleteAsync();

            return new UserPlanResponse(existingUserPlan);
        }
        catch (Exception ex)
        {
            // Manejo del error y registro, si es necesario
            return new UserPlanResponse($"An error occurred while updating the User Plan: {ex.Message}");
        }
    }

    public async Task<UserPlanResponse> DeleteAsync(int userPlanId)
    {
        var existingUserPlan = await _userPlanRepository.FindByIdAsync(userPlanId);

        if (existingUserPlan == null)
            return new UserPlanResponse("User Plan not found.");

        try
        {
            _userPlanRepository.Remove(existingUserPlan);
            await _unitOfWork.CompleteAsync();

            return new UserPlanResponse(existingUserPlan);
        }
        catch (Exception ex)
        {
            // Manejo del error y registro, si es necesario
            return new UserPlanResponse($"An error occurred while deleting the User Plan: {ex.Message}");
        }
    }
}