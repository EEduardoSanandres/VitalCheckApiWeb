using Swashbuckle.AspNetCore.Annotations;

namespace VitalCheckWeb.API.VitalCheck.Resources;

public class MedicineResource
{
    [SwaggerSchema("Medicine Identifier")]
    public int MedicineID { get; set; }
    
    [SwaggerSchema("Commercial Name")]
    public string CommercialName { get; set; }
    
    [SwaggerSchema("Generic Name")]
    public string GenericName { get; set; }
    
    [SwaggerSchema("Medicine Type")]
    public MedicineTypeResource MedicineType { get; set; }
    
    [SwaggerSchema("Cost Price")]
    public decimal CostPrice { get; set; }
}
