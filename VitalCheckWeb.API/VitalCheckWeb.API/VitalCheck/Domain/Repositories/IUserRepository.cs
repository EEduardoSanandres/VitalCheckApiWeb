using VitalCheckWeb.API.VitalCheck.Domain.Models;

namespace VitalCheckWeb.API.VitalCheck.Domain.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> ListAsync();
    Task AddAsync(User user);
    Task<User> FindByIdAsync(int userId);
    Task<IEnumerable<User>> ListByUserPlanIdAsync(int userPlanId);
    Task<IEnumerable<User>> ListByUserTypeIdAsync(int userTypeId);
    void Update(User user);
    void Remove(User user);
}