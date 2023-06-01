namespace VitalCheckWeb.API.VitalCheck.Resources;

public class SaveDispatchResource
{
    public int CompanyID { get; set; }
    public int ProviderID { get; set; }
    public int MedicineID { get; set; }
    public int Quantity { get; set; }
    public string Description { get; set; }
    public DateTime EntryDate { get; set; }
    public DateTime ExpiryDate { get; set; }
}