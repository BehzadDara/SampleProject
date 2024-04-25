using SampleProject.Domain.BaseInterfaces;
using System.Security.Claims;

namespace SampleProject.API;

public class CurrentUser(IHttpContextAccessor httpContextAccessor) : ICurrentUser
{
    public string UserName => httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
}
