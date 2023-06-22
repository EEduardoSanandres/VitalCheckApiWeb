using VitalCheckWeb.API.Security.Domain.Models;

namespace VitalCheckWeb.API.VitalCheck.Domain.Models;

public class UserType
{
    public int UserTypeID { get; set; }
    public string TypeName { get; set; }
        
    // Relationships
    public IList<User> Users { get; set; } = new List<User>();
}