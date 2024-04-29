using SampleProject.Application.BaseExceptions;
using SampleProject.Application.BaseFeatures;
using System.Text.Json;

namespace SampleProject.API.BaseMiddlewares;

public class GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (BaseValidationException ex)
        {
            var result = new BaseResult();
            result.BadRequest(ex.Errors);

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(result));
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);

            var result = new BaseResult();
            result.InternalServerError();

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(result));
        }
    }
}
