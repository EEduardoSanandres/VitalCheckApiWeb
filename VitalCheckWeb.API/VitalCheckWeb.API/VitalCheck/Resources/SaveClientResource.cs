using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace VitalCheckWeb.API.VitalCheck.Resources;

[SwaggerSchema(Required = new []{"DNI", "FirstName", "LastName"})]
public class SaveClientResource
{
    [SwaggerSchema("Número de DNI")]
    [Required(ErrorMessage = "El número de DNI es obligatorio.")]
    public int DNI { get; set; }
    
    [SwaggerSchema("Nombre", Description = "El nombre debe tener entre 2 y 50 caracteres.")]
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 50 caracteres.")]
    public string FirstName { get; set; }
    
    [SwaggerSchema("Apellido", Description = "El apellido debe tener entre 2 y 50 caracteres.")]
    [Required(ErrorMessage = "El apellido es obligatorio.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "El apellido debe tener entre 2 y 50 caracteres.")]
    public string LastName { get; set; }
}
