using System.ComponentModel.DataAnnotations;

namespace VitalCheckWeb.API.Security.Domain.Services.Communication;

public class RegisterRequest
{
    [Required]
    [MaxLength(50)]
    public string UserName { get; set; }

    [Required]
    [EmailAddress]
    [MaxLength(100)]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public long RUC { get; set; }

    [Required]
    public int UserPlanID { get; set; }

    [Required]
    public int UserTypeID { get; set; }
}
