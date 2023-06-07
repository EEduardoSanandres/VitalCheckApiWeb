using VitalCheckWeb.API.Shared.Domain.Services.Communication;
using VitalCheckWeb.API.VitalCheck.Domain.Models;

namespace VitalCheckWeb.API.VitalCheck.Domain.Services.Communication;

public class SaleResponse : BaseResponse<Sale>
{
    public SaleResponse(string message) : base(message)
    {
        
    }
    public SaleResponse(Sale resource) : base(resource)
    {
        
    }
}