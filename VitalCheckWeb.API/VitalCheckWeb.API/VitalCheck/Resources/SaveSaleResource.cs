using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace VitalCheckWeb.API.VitalCheck.Resources;

[SwaggerSchema(Required = new []{"UserID", "ClientID", "MedicineID", "Quantity", "TotalPrice", "Date"})]
public class SaveSaleResource
{
    [SwaggerSchema("ID Usuario")]
    [Required(ErrorMessage = "El ID de usuario es obligatorio.")]
    public int UserID { get; set; }
    
    [SwaggerSchema("ID Cliente")]
    [Required(ErrorMessage = "El ID de cliente es obligatorio.")]
    public int ClientID { get; set; }
    
    [SwaggerSchema("ID Medicamento")]
    [Required(ErrorMessage = "El ID de medicamento es obligatorio.")]
    public int MedicineID { get; set; }
    
    [SwaggerSchema("Cantidad", Description = "La cantidad debe ser mayor que 0.")]
    [Required(ErrorMessage = "La cantidad es obligatoria.")]
    [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que 0.")]
    public int Quantity { get; set; }
    
    [SwaggerSchema("Precio total", Description = "El precio total debe ser mayor o igual a 0.")]
    [Required(ErrorMessage = "El precio total es obligatorio.")]
    [Range(0, double.MaxValue, ErrorMessage = "El precio total debe ser mayor o igual a 0.")]
    public decimal TotalPrice { get; set; }
    
    [SwaggerSchema("Fecha")]
    [Required(ErrorMessage = "La fecha es obligatoria.")]
    public DateTime Date { get; set; }
}
