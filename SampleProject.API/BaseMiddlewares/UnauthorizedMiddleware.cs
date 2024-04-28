using SampleProject.Application.BaseFeatures;
using System.Net;
using System.Text.Json;

namespace SampleProject.API.BaseMiddlewares;

public class UnauthorizedMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        await next(context);

        if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
        {
            var result = new BaseResult();
            result.Unauthorized();

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;

            await context.Response.WriteAsync(JsonSerializer.Serialize(result));
        }
    }
}
