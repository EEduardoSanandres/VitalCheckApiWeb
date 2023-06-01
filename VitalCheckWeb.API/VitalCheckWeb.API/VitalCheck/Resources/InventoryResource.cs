namespace VitalCheckWeb.API.VitalCheck.Resources;

public class InventoryResource
{
    public int InventoryID { get; set; }
    public UserResource User { get; set; }
    public MedicineResource Medicine { get; set; }
    public int Quantity { get; set; }
    public decimal SalePrice { get; set; }
}