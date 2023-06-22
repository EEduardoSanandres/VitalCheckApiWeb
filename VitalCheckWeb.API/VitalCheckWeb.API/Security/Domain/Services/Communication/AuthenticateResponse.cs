namespace VitalCheckWeb.API.Security.Domain.Services.Communication;

public class AuthenticateResponse
{
    public int UserID { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
}