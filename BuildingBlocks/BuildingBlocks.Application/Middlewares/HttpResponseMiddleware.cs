using BuildingBlocks.Application.Exceptions;
using Microsoft.AspNetCore.Http;

namespace BuildingBlocks.Application.Middlewares;

public class HttpResponseMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        await next(context);

        switch (context.Response.StatusCode)
        {
            case StatusCodes.Status401Unauthorized: throw new UnauthorizedException(Resources.Messages.Unauthorized);
            case StatusCodes.Status403Forbidden: throw new ForbiddenException(Resources.Messages.Forbidden);
            case StatusCodes.Status405MethodNotAllowed: throw new MethodNotAllowedException(Resources.Messages.MethodNotAllowed);
        }
    }
}
