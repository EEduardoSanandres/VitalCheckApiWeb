using System.ComponentModel.DataAnnotations.Schema;
using VitalCheckWeb.API.Security.Domain.Models;

namespace VitalCheckWeb.API.VitalCheck.Domain.Models;

public class Dispatch
{
    public int DispatchID { get; set; }
    public int Quantity { get; set; }
    public string Description { get; set; }
    public DateTime EntryDate { get; set; }
    public DateTime ExpiryDate { get; set; }

    // Relationships
    
    public int User1ID { get; set; }
    [ForeignKey("User1ID")]
    public User User1 { get; set; }
    [ForeignKey("User2ID")]
    public int User2ID { get; set; }
    public User User2 { get; set; }
    public int MedicineID { get; set; }
    public Medicine Medicine { get; set; }
}