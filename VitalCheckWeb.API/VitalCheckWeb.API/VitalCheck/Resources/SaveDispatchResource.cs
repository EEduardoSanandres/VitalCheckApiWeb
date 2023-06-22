using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace VitalCheckWeb.API.VitalCheck.Resources;

[SwaggerSchema(Required = new []{"User1ID", "User2ID", "MedicineID", "Quantity", "EntryDate", "ExpiryDate"})]
public class SaveDispatchResource
{
    [SwaggerSchema("ID Usuario 1")]
    [Required(ErrorMessage = "El ID del usuario 1 es obligatorio.")]
    public int User1ID { get; set; }
    
    [SwaggerSchema("ID Usuario 2")]
    [Required(ErrorMessage = "El ID del usuario 2 es obligatorio.")]
    public int User2ID { get; set; }
    
    [SwaggerSchema("ID Medicamento")]
    [Required(ErrorMessage = "El ID del medicamento es obligatorio.")]
    public int MedicineID { get; set; }
    
    [SwaggerSchema("Cantidad", Description = "La cantidad debe ser mayor que 0.")]
    [Required(ErrorMessage = "La cantidad es obligatoria.")]
    [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que 0.")]
    public int Quantity { get; set; }
    
    [SwaggerSchema("Descripción", Description = "La descripción debe tener como máximo 100 caracteres.")]
    [StringLength(100, ErrorMessage = "La descripción debe tener como máximo 100 caracteres.")]
    public string Description { get; set; }
    
    [SwaggerSchema("Fecha de entrada")]
    [Required(ErrorMessage = "La fecha de entrada es obligatoria.")]
    public DateTime EntryDate { get; set; }
    
    [SwaggerSchema("Fecha de vencimiento")]
    [Required(ErrorMessage = "La fecha de vencimiento es obligatoria.")]
    public DateTime ExpiryDate { get; set; }
}
