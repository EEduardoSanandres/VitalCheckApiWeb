using VitalCheckWeb.API.VitalCheck.Resources;

namespace VitalCheckWeb.API.Security.Resources;

public class UserResource
{
    public int UserID { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public long RUC { get; set; }
    public UserPlanResource UserPlan { get; set; }
    public UserTypeResource UserType { get; set; }
}