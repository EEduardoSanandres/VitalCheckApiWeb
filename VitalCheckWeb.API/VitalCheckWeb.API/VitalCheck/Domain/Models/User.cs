namespace VitalCheckWeb.API.VitalCheck.Domain.Models;

public class User
{
    public int UserID { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public long RUC { get; set; }
    public DateTime RegistrationDate { get; set; }

    // Relationships
    
    public int UserPlanID { get; set; }
    public UserPlan UserPlan { get; set; }
    
    public IList<Inventory> Inventories { get; set; } = new List<Inventory>();
    public IList<Provider> Providers { get; set; } = new List<Provider>();
    public IList<Company> Companies { get; set; } = new List<Company>();
}