using VitalCheckWeb.API.VitalCheck.Domain.Models;

namespace VitalCheckWeb.API.VitalCheck.Domain.Repositories;

public interface IUserPlanRepository
{
    Task<IEnumerable<UserPlan>> ListAsync();
    Task AddAsync(UserPlan userPlan);
    Task<UserPlan> FindByIdAsync(int userPlanId);
    void Update(UserPlan userPlan);
    void Remove(UserPlan userPlan);
}