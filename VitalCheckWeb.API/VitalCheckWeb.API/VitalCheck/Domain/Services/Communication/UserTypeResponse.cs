using VitalCheckWeb.API.Shared.Domain.Services.Communication;
using VitalCheckWeb.API.VitalCheck.Domain.Models;

namespace VitalCheckWeb.API.VitalCheck.Domain.Services.Communication;

public class UserTypeResponse : BaseResponse<UserType>
{
    public UserTypeResponse(string message) : base(message)
    {
        
    }
    public UserTypeResponse(UserType resource) : base(resource)
    {
        
    }
}