namespace VitalCheckWeb.API.VitalCheck.Domain.Models;

public class Inventory
{
    public int InventoryID { get; set; }
    public int Quantity { get; set; }
    public decimal SalePrice { get; set; }

    // Relationships
    public int UserID { get; set; }
    public User User { get; set; }
    
    public int MedicineID { get; set; }
    public Medicine Medicine { get; set; }
}