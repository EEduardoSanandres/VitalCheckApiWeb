using Swashbuckle.AspNetCore.Annotations;
using VitalCheckWeb.API.Security.Resources;

namespace VitalCheckWeb.API.VitalCheck.Resources;

public class InventoryResource
{
    [SwaggerSchema("Inventory Identifier")]
    public int InventoryID { get; set; }
    
    [SwaggerSchema("User")]
    public UserResource User { get; set; }
    
    [SwaggerSchema("Medicine")]
    public MedicineResource Medicine { get; set; }
    
    [SwaggerSchema("Quantity")]
    public int Quantity { get; set; }
    
    [SwaggerSchema("Sale Price")]
    public decimal SalePrice { get; set; }
}
