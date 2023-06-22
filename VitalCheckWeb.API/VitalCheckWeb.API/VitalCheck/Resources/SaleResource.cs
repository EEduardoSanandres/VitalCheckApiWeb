using Swashbuckle.AspNetCore.Annotations;
using VitalCheckWeb.API.Security.Resources;

namespace VitalCheckWeb.API.VitalCheck.Resources;

public class SaleResource
{
    [SwaggerSchema("Sale Identifier")]
    public int SaleID { get; set; }
    
    [SwaggerSchema("User")]
    public UserResource User { get; set; }
    
    [SwaggerSchema("Client")]
    public ClientResource Client { get; set; }
    
    [SwaggerSchema("Medicine")]
    public MedicineResource Medicine { get; set; }
    
    [SwaggerSchema("Quantity")]
    public int Quantity { get; set; }
    
    [SwaggerSchema("Total Price")]
    public decimal TotalPrice { get; set; }
    
    [SwaggerSchema("Date")]
    public DateTime Date { get; set; }
}
