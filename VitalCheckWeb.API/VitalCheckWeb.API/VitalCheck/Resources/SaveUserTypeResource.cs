using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace VitalCheckWeb.API.VitalCheck.Resources;

[SwaggerSchema(Required = new []{"TypeName"})]
public class SaveUserTypeResource
{
    [SwaggerSchema("Nombre del tipo de Usuario")]
    [Required(ErrorMessage = "El nombre del tipo de usuario es obligatorio.")]
    public string TypeName { get; set; }
}