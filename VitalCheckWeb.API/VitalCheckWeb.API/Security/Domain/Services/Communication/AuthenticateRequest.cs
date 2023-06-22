using System.ComponentModel.DataAnnotations;

namespace VitalCheckWeb.API.Security.Domain.Services.Communication;

public class AuthenticateRequest
{
    [Required] 
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }
}