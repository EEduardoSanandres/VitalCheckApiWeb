using Swashbuckle.AspNetCore.Annotations;

namespace VitalCheckWeb.API.VitalCheck.Resources;

public class UserTypeResource
{
    [SwaggerSchema("User Type Identifier")]
    public int UserTypeID { get; set; }
    
    [SwaggerSchema("Type Name")]
    public string TypeName { get; set; }
}