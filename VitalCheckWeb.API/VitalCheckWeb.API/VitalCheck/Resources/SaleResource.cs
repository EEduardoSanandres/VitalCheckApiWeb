namespace VitalCheckWeb.API.VitalCheck.Resources;

public class SaleResource
{
    public int SaleID { get; set; }
    public CompanyResource Company { get; set; }
    public ClientResource Client { get; set; }
    public MedicineResource Medicine { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime Date { get; set; }
}