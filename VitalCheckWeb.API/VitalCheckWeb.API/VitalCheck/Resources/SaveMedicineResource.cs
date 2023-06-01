namespace VitalCheckWeb.API.VitalCheck.Resources;

public class SaveMedicineResource
{
    public string CommercialName { get; set; }
    public string GenericName { get; set; }
    public int MedicineTypeID { get; set; }
    public decimal CostPrice { get; set; }
}