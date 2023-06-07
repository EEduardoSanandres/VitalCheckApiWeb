using VitalCheckWeb.API.Shared.Domain.Services.Communication;
using VitalCheckWeb.API.VitalCheck.Domain.Models;

namespace VitalCheckWeb.API.VitalCheck.Domain.Services.Communication;

public class MedicineTypeResponse : BaseResponse<MedicineType>
{
    public MedicineTypeResponse(string message) : base(message)
    {
        
    }
    public MedicineTypeResponse(MedicineType resource) : base(resource)
    {
        
    }
}