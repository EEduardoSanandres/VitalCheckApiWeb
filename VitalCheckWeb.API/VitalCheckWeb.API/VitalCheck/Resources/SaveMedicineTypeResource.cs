using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace VitalCheckWeb.API.VitalCheck.Resources;

[SwaggerSchema(Required = new []{"TypeName"})]
public class SaveMedicineTypeResource
{
    [SwaggerSchema("Nombre del tipo de Medicamento")]
    [Required(ErrorMessage = "El nombre del tipo de medicamento es obligatorio.")]
    public string TypeName { get; set; }
}