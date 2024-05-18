using Microsoft.AspNetCore.Http;
using BuildingBlocks.Application.Exceptions;

namespace BuildingBlocks.Application.Middlewares;

public class HttpResponseMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        await next(context);

        switch (context.Response.StatusCode)
        {
            case StatusCodes.Status401Unauthorized: throw new UnauthorizedException();
            case StatusCodes.Status403Forbidden: throw new ForbiddenException();
            case StatusCodes.Status405MethodNotAllowed: throw new MethodNotAllowedException();
            case StatusCodes.Status409Conflict: throw new ConflictException();
        }
    }
}
