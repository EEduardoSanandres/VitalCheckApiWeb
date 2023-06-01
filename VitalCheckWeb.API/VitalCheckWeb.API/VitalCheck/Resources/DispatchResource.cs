namespace VitalCheckWeb.API.VitalCheck.Resources;

public class DispatchResource
{
    public int DispatchID { get; set; }
    public CompanyResource Company { get; set; }
    public ProviderResource Provider { get; set; }
    public MedicineResource Medicine { get; set; }
    public int Quantity { get; set; }
    public string Description { get; set; }
    public DateTime EntryDate { get; set; }
    public DateTime ExpiryDate { get; set; }
}