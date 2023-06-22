using VitalCheckWeb.API.VitalCheck.Domain.Models;
using VitalCheckWeb.API.VitalCheck.Domain.Services.Communication;

namespace VitalCheckWeb.API.VitalCheck.Domain.Services;

public interface IUserTypeService
{
    Task<IEnumerable<UserType>> ListAsync();
    Task<UserTypeResponse> SaveAsync(UserType userType);
    Task<UserTypeResponse> UpdateAsync(int userTypeId, UserType userType);
    Task<UserTypeResponse> DeleteAsync(int userTypeId);
}