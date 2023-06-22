using Microsoft.Extensions.Options;
using VitalCheckWeb.API.Security.Authorization.Handlers.Interfaces;
using VitalCheckWeb.API.Security.Authorization.Settings;
using VitalCheckWeb.API.Security.Domain.Services;

namespace VitalCheckWeb.API.Security.Authorization.Middleware;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly AppSettings _appSettings;
    
    public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
    {
        _next = next;
        _appSettings = appSettings.Value;;
    }
    
    public async Task Invoke(HttpContext context, IUserService userService, IJwtHandler handler)
    {
        var token = 
            context.Request.Headers["Authorization"].FirstOrDefault()?.Split("").Last();
        var userId = handler.ValidateToken(token);
        if (userId != null)
        {
            // attach user to context on successful jwt validation
            context.Items["User"] = await userService.GetByIdAsync(userId.Value);
        }
        await _next(context);
    }
}