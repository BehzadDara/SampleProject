using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SampleProject.Application.BaseExceptions;
using SampleProject.Application.BaseFeatures;
using System.Text.Json;

namespace SampleProject.Application;

public class GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (BadRequestException ex)
        {
            var result = new BaseResult();
            result.BadRequest(ex.Errors);

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(result));
        }
        catch (UnauthorizedException)
        {
            var result = new BaseResult();
            result.Unauthorized();

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(result));
        }
        catch (ForbiddenException)
        {
            var result = new BaseResult();
            result.Forbidden();

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(result));
        }
        catch (NotFoundException)
        {
            var result = new BaseResult();
            result.NotFound();

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(result));
        }
        catch (MethodNotAllowedException)
        {
            var result = new BaseResult();
            result.MethodNotAllowed();

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(result));
        }
        catch (TooManyRequestException)
        {
            var result = new BaseResult();
            result.TooManyRequest();

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
