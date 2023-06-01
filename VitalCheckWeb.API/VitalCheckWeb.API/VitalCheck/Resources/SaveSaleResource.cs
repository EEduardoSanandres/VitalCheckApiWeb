namespace VitalCheckWeb.API.VitalCheck.Resources;

public class SaveSaleResource
{
    public int CompanyID { get; set; }
    public int ClientID { get; set; }
    public int MedicineID { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime Date { get; set; }
}