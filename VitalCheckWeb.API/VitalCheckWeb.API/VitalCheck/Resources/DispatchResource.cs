using Swashbuckle.AspNetCore.Annotations;
using VitalCheckWeb.API.Security.Resources;

namespace VitalCheckWeb.API.VitalCheck.Resources;

public class DispatchResource
{
    [SwaggerSchema("Dispatch Identifier")]
    public int DispatchID { get; set; }
    
    [SwaggerSchema("User 1")]
    public UserResource User1 { get; set; }
    
    [SwaggerSchema("User 2")]
    public UserResource User2 { get; set; }
    
    [SwaggerSchema("Medicine")]
    public MedicineResource Medicine { get; set; }
    
    [SwaggerSchema("Quantity")]
    public int Quantity { get; set; }
    
    [SwaggerSchema("Description")]
    public string Description { get; set; }
    
    [SwaggerSchema("Entry Date")]
    public DateTime EntryDate { get; set; }
    
    [SwaggerSchema("Expiry Date")]
    public DateTime ExpiryDate { get; set; }
}
