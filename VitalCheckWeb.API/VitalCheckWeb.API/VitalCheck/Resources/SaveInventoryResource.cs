namespace VitalCheckWeb.API.VitalCheck.Resources;

public class SaveInventoryResource
{
    public int UserID { get; set; }
    public int MedicineID { get; set; }
    public int Quantity { get; set; }
    public decimal SalePrice { get; set; }
}