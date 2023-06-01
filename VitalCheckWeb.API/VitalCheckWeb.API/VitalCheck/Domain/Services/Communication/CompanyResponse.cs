using VitalCheckWeb.API.Shared.Domain.Services.Communication;
using VitalCheckWeb.API.VitalCheck.Domain.Models;

namespace VitalCheckWeb.API.VitalCheck.Domain.Services.Communication;

public class CompanyResponse: BaseResponse<Company>
{
    public CompanyResponse(string message) : base(message)
    {
    }
    public CompanyResponse(Company resource) : base(resource)
    {
    }
}