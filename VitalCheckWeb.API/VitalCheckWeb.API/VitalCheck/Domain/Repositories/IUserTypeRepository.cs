using VitalCheckWeb.API.VitalCheck.Domain.Models;

namespace VitalCheckWeb.API.VitalCheck.Domain.Repositories;

public interface IUserTypeRepository
{
    Task<IEnumerable<UserType>> ListAsync();
    Task AddAsync(UserType userType);
    Task<UserType> FindByIdAsync(int userTypeId);
    void Update(UserType userType);
    void Remove(UserType userType);
}