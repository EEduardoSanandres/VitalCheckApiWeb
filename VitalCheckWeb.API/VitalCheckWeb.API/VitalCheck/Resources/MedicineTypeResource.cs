using Swashbuckle.AspNetCore.Annotations;

namespace VitalCheckWeb.API.VitalCheck.Resources;

public class MedicineTypeResource
{
    [SwaggerSchema("Medicine Type Identifier")]
    public int MedicineTypeID { get; set; }
    
    [SwaggerSchema("Type Name")]
    public string TypeName { get; set; }
}