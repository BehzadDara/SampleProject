using BuildingBlocks.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BuildingBlocks.API.Configs;

public class CurrentUser(IHttpContextAccessor httpContextAccessor) : ICurrentUser
{
    public string IPAddress => httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
    public string UserName => httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
}
