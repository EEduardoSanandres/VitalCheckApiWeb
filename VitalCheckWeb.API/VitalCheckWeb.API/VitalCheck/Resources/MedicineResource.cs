namespace VitalCheckWeb.API.VitalCheck.Resources;

public class MedicineResource
{
    public int MedicineID { get; set; }
    public string CommercialName { get; set; }
    public string GenericName { get; set; }
    public MedicineTypeResource MedicineType { get; set; }
    public decimal CostPrice { get; set; }
}