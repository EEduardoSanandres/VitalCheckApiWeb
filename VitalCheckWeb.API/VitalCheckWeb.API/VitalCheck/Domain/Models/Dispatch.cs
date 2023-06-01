namespace VitalCheckWeb.API.VitalCheck.Domain.Models;

public class Dispatch
{
    public int DispatchID { get; set; }
    public int Quantity { get; set; }
    public string Description { get; set; }
    public DateTime EntryDate { get; set; }
    public DateTime ExpiryDate { get; set; }

    // Relationships
    public int CompanyID { get; set; }
    public Company Company { get; set; }
    
    public int ProviderID { get; set; }
    public Provider Provider { get; set; }
    
    public int MedicineID { get; set; }
    public Medicine Medicine { get; set; }
}