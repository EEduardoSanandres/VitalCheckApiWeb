using VitalCheckWeb.API.Shared.Domain.Services.Communication;
using VitalCheckWeb.API.VitalCheck.Domain.Models;

namespace VitalCheckWeb.API.VitalCheck.Domain.Services.Communication;

public class ClientResponse : BaseResponse<Client>
{
    public ClientResponse(string message) : base(message)
    {
    }
    public ClientResponse(Client resource) : base(resource)
    {
    }
}