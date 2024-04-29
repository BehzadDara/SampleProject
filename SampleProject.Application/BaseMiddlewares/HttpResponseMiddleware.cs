using Microsoft.AspNetCore.Http;
using SampleProject.Application.BaseExceptions;

namespace SampleProject.Application.BaseMiddlewares;

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
        }
    }
}
