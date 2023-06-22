using VitalCheckWeb.API.Security.Domain.Models;

namespace VitalCheckWeb.API.Security.Authorization.Handlers.Interfaces;

public interface IJwtHandler
{
    public string GenerateToken(User user);
    public int? ValidateToken(string token);
}