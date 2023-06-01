using VitalCheckWeb.API.Shared.Domain.Services.Communication;
using VitalCheckWeb.API.VitalCheck.Domain.Models;

namespace VitalCheckWeb.API.VitalCheck.Domain.Services.Communication;

public class UserResponse : BaseResponse<User>
{
    public UserResponse(string message) : base(message)
    {
        
    }
    public UserResponse(User resource) : base(resource)
    {
        
    }
}