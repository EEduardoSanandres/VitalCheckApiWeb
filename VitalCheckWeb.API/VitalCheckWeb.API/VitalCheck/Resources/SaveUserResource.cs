namespace VitalCheckWeb.API.VitalCheck.Resources;

public class SaveUserResource
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public long RUC { get; set; }
    public DateTime RegistrationDate { get; set; }
    public int UserPlanID { get; set; }
}