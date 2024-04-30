using SampleProject.Domain.BaseInterfaces;
using System.Security.Claims;

namespace SampleProject.API.Configs;

public class CurrentUser(IHttpContextAccessor httpContextAccessor) : ICurrentUser
{
    public string IPAddress => httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
    public string UserName => httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
}
