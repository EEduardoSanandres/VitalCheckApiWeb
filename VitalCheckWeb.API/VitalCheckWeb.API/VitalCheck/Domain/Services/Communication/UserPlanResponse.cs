using VitalCheckWeb.API.Shared.Domain.Services.Communication;
using VitalCheckWeb.API.VitalCheck.Domain.Models;

namespace VitalCheckWeb.API.VitalCheck.Domain.Services.Communication;

public class UserPlanResponse : BaseResponse<UserPlan>
{
    public UserPlanResponse(string message) : base(message)
    {
        
    }
    public UserPlanResponse(UserPlan resource) : base(resource)
    {
        
    }
}