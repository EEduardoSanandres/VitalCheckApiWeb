using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace VitalCheckWeb.API.VitalCheck.Resources;

[SwaggerSchema(Required = new []{"UserName", "Email", "Password", "RUC", "UserPlanID", "UserTypeID"})]
public class SaveUserResource
{
    [SwaggerSchema("Nombre de usuario")]
    [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
    public string UserName { get; set; }
    
    [SwaggerSchema("Correo electrónico", Description = "Debe ser un correo electrónico válido.")]
    [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
    [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
    public string Email { get; set; }
    
    [SwaggerSchema("Contraseña", Description = "La contraseña debe tener entre 6 y 50 caracteres.")]
    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    [StringLength(50, MinimumLength = 6, ErrorMessage = "La contraseña debe tener entre 6 y 50 caracteres.")]
    public string Password { get; set; }
    
    [SwaggerSchema("RUC", Description = "El RUC debe tener 11 dígitos.")]
    [Required(ErrorMessage = "El RUC es obligatorio.")]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "El RUC debe tener 11 dígitos.")]
    public long RUC { get; set; }

    [SwaggerSchema("ID Plan de usuario")]
    [Required(ErrorMessage = "El ID del plan de usuario es obligatorio.")]
    public int UserPlanID { get; set; }
    
    [SwaggerSchema("ID Tipo de usuario")]
    [Required(ErrorMessage = "El ID del tipo de usuario es obligatorio.")]
    public int UserTypeID { get; set; }
}
