namespace VitalCheckWeb.API.VitalCheck.Domain.Models;

public class Client
{
    public int ClientID { get; set; }
    public int DNI { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    // Relationships
    public IList<Sale> Sales { get; set; } = new List<Sale>();
}