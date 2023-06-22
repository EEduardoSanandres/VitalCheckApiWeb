using VitalCheckWeb.API.Security.Domain.Models;

namespace VitalCheckWeb.API.VitalCheck.Domain.Models;

public class UserPlan
{
        public int UserPlanID { get; set; }
        public string PlanName { get; set; }
        
        // Relationships
        public IList<User> Users { get; set; } = new List<User>();
}