using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using VitalCheckWeb.API.VitalCheck.Domain.Models;

namespace VitalCheckWeb.API.Security.Domain.Models;

public class User
{
    public int UserID { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    [JsonIgnore] 
    public string Password { get; set; }
    public long RUC { get; set; }

    // Relationships
    
    public int UserPlanID { get; set; }
    public UserPlan UserPlan { get; set; }
    
    public int UserTypeID { get; set; }
    public UserType UserType { get; set; }
    
    public IList<Sale> Sales { get; set; } = new List<Sale>();

    public IList<Inventory> Inventories { get; set; } = new List<Inventory>();
    
    [InverseProperty("User1")]
    public IList<Dispatch> Dispatches1 { get; set; } = new List<Dispatch>();

    [InverseProperty("User2")]
    public IList<Dispatch> Dispatches2 { get; set; } = new List<Dispatch>();
}