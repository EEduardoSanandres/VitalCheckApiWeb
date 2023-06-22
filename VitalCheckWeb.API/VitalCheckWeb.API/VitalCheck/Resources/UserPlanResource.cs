using Swashbuckle.AspNetCore.Annotations;

namespace VitalCheckWeb.API.VitalCheck.Resources;

public class UserPlanResource
{
    [SwaggerSchema("User Plan Identifier")]
    public int UserPlanID { get; set; }
    
    [SwaggerSchema("Plan Name")]
    public string PlanName { get; set; }
}