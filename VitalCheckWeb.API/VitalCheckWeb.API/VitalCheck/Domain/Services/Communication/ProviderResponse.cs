using VitalCheckWeb.API.Shared.Domain.Services.Communication;
using VitalCheckWeb.API.VitalCheck.Domain.Models;

namespace VitalCheckWeb.API.VitalCheck.Domain.Services.Communication;

public class ProviderResponse : BaseResponse<Provider>
{
    public ProviderResponse(string message) : base(message)
    {
        
    }
    public ProviderResponse(Provider resource) : base(resource)
    {
        
    }
}