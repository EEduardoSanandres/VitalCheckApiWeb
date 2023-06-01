using VitalCheckWeb.API.VitalCheck.Domain.Models;
using VitalCheckWeb.API.VitalCheck.Domain.Services.Communication;

namespace VitalCheckWeb.API.VitalCheck.Domain.Services;

public interface IUserService
{
    Task<IEnumerable<User>> ListAsync();
    Task<IEnumerable<User>> ListByUserPlanIdAsync(int userPlanId);
    Task<UserResponse> SaveAsync(User user);
    Task<UserResponse> UpdateAsync(int userId, User user);
    Task<UserResponse> DeleteAsync(int userId);
}