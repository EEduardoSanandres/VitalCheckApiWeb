using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace VitalCheckWeb.API.VitalCheck.Resources;

[SwaggerSchema(Required = new []{"PlanName"})]
public class SaveUserPlanResource
{
    [SwaggerSchema("Nombre del plan de Usuario")]
    [Required(ErrorMessage = "El nombre del plan es obligatorio.")]
    public string PlanName { get; set; }
}