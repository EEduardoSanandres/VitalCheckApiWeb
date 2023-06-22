namespace VitalCheckWeb.API.Security.Domain.Services.Communication;

public class UpdateRequest
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public long RUC { get; set; }
    public int UserPlanID { get; set; }
    public int UserTypeID { get; set; }
}