using Swashbuckle.AspNetCore.Annotations;

namespace VitalCheckWeb.API.VitalCheck.Resources;

public class ClientResource
{
    [SwaggerSchema("Client Identifier")]
    public int ClientID { get; set; }
    
    [SwaggerSchema("DNI")]
    public int DNI { get; set; }
    
    [SwaggerSchema("First Name")]
    public string FirstName { get; set; }
    
    [SwaggerSchema("Last Name")]
    public string LastName { get; set; }
}
