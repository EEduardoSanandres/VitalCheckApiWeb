using VitalCheckWeb.API.Shared.Domain.Services.Communication;
using VitalCheckWeb.API.VitalCheck.Domain.Models;

namespace VitalCheckWeb.API.VitalCheck.Domain.Services.Communication;

public class DispatchResponse : BaseResponse<Dispatch>
{
    public DispatchResponse(string message) : base(message)
    {
    }
    public DispatchResponse(Dispatch resource) : base(resource)
    {
    }
}