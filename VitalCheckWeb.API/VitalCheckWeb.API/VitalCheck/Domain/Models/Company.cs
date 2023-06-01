namespace VitalCheckWeb.API.VitalCheck.Domain.Models;

public class Company
{
    public int CompanyID { get; set; }

    // Relationships
    public int UserID { get; set; }
    public User User { get; set; }
    
    public IList<Dispatch> Dispatches { get; set; } = new List<Dispatch>();
    public IList<Sale> Sales { get; set; } = new List<Sale>();
}