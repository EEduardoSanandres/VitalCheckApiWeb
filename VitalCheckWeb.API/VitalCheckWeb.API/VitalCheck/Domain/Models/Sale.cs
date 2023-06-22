using VitalCheckWeb.API.Security.Domain.Models;

namespace VitalCheckWeb.API.VitalCheck.Domain.Models;

public class Sale
{
    public int SaleID { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime Date { get; set; }

    // Relationships
    public int UserID { get; set; }
    public User User { get; set; }
    
    public int ClientID { get; set; }
    public Client Client { get; set; }
    
    public int MedicineID { get; set; }
    public Medicine Medicine { get; set; }

}