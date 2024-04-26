using SampleProject.Application.BaseFeatures;
using System.Net;
using System.Text.Json;

namespace SampleProject.API.BaseMiddlewares;

public class InternalServerErrorMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            var result = new BaseResult();
            result.InternalServerError(ex.Message);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            await context.Response.WriteAsync(JsonSerializer.Serialize(result));
        }
    }
}
