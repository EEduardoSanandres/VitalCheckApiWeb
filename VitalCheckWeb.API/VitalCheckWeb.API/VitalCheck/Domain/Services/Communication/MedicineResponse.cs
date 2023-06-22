using VitalCheckWeb.API.Shared.Domain.Services.Communication;
using VitalCheckWeb.API.VitalCheck.Domain.Models;

namespace VitalCheckWeb.API.VitalCheck.Domain.Services.Communication;

public class MedicineResponse : BaseResponse<Medicine>
{
    public MedicineResponse(string message) : base(message)
    {
        
    }
    public MedicineResponse(Medicine resource) : base(resource)
    {
        
    }
}