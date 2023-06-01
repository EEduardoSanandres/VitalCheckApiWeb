namespace VitalCheckWeb.API.VitalCheck.Domain.Models;

public class Provider
{
    public int ProviderID { get; set; }

    // Relationships
    public int UserID { get; set; }
    public User User { get; set; }
    
    public IList<Dispatch> Dispatches { get; set; } = new List<Dispatch>();
}