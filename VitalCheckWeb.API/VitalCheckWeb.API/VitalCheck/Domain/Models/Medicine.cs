namespace VitalCheckWeb.API.VitalCheck.Domain.Models;

public class Medicine
{
    public int MedicineID { get; set; }
    public string CommercialName { get; set; }
    public string GenericName { get; set; }
    
    public decimal CostPrice { get; set; }

    // Relationships
    public int MedicineTypeID { get; set; }
    public MedicineType MedicineType { get; set; }
    
    public IList<Inventory> Inventories { get; set; } = new List<Inventory>();
    public IList<Dispatch> Dispatches { get; set; } = new List<Dispatch>();
    public IList<Sale> Sales { get; set; } = new List<Sale>();
}