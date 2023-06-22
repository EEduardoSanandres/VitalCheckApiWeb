using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace VitalCheckWeb.API.VitalCheck.Resources;

[SwaggerSchema(Required = new []{"CommercialName", "GenericName", "MedicineTypeID", "CostPrice"})]
public class SaveMedicineResource
{
    [SwaggerSchema("Nombre comercial")]
    [Required(ErrorMessage = "El nombre comercial es obligatorio.")]
    public string CommercialName { get; set; }
    
    [SwaggerSchema("Nombre genérico")]
    [Required(ErrorMessage = "El nombre genérico es obligatorio.")]
    public string GenericName { get; set; }
    
    [SwaggerSchema("ID Tipo de medicamento")]
    [Required(ErrorMessage = "El ID del tipo de medicamento es obligatorio.")]
    public int MedicineTypeID { get; set; }
    
    [SwaggerSchema("Precio de costo", Description = "El precio de costo debe ser mayor o igual a 0.")]
    [Required(ErrorMessage = "El precio de costo es obligatorio.")]
    [Range(0, double.MaxValue, ErrorMessage = "El precio de costo debe ser mayor o igual a 0.")]
    public decimal CostPrice { get; set; }
}
