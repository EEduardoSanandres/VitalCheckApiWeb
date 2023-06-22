using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace VitalCheckWeb.API.VitalCheck.Resources;

[SwaggerSchema(Required = new []{"UserID", "MedicineID", "Quantity", "SalePrice"})]
public class SaveInventoryResource
{
    [SwaggerSchema("ID Usuario")]
    [Required(ErrorMessage = "El ID de usuario es obligatorio.")]
    public int UserID { get; set; }
    
    [SwaggerSchema("ID Medicamento")]
    [Required(ErrorMessage = "El ID de medicamento es obligatorio.")]
    public int MedicineID { get; set; }
    
    [SwaggerSchema("Cantidad", Description = "La cantidad debe ser mayor que 0.")]
    [Required(ErrorMessage = "La cantidad es obligatoria.")]
    [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que 0.")]
    public int Quantity { get; set; }
    
    [SwaggerSchema("Precio de venta", Description = "El precio de venta debe ser mayor o igual a 0.")]
    [Required(ErrorMessage = "El precio de venta es obligatorio.")]
    [Range(0, double.MaxValue, ErrorMessage = "El precio de venta debe ser mayor o igual a 0.")]
    public decimal SalePrice { get; set; }
}
