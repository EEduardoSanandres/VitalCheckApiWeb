using VitalCheckWeb.API.VitalCheck.Domain.Models;
using VitalCheckWeb.API.VitalCheck.Domain.Services.Communication;

namespace VitalCheckWeb.API.VitalCheck.Domain.Services;

public interface IUserPlanService
{
    Task<IEnumerable<UserPlan>> ListAsync();
    Task<UserPlanResponse> SaveAsync(UserPlan userPlan);
    Task<UserPlanResponse> UpdateAsync(int userPlanId, UserPlan userPlan);
    Task<UserPlanResponse> DeleteAsync(int userPlanId);
}