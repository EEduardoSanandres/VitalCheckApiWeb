using VitalCheckWeb.API.Shared.Domain.Services.Communication;
using VitalCheckWeb.API.VitalCheck.Domain.Models;

namespace VitalCheckWeb.API.VitalCheck.Domain.Services.Communication;

public class InventoryResponse : BaseResponse<Inventory>
{
    public InventoryResponse(string message) : base(message)
    {
        
    }
    public InventoryResponse(Inventory resource) : base(resource)
    {
        
    }
}