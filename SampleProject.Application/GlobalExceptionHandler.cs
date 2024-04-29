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
        var result = new BaseResult();

        try
        {
            await next(context);
        }
        catch (BadRequestException ex)
        {
            result.BadRequest(ex.Errors);
            await SetContext(context, result, StatusCodes.Status400BadRequest);
        }
        catch (UnauthorizedException)
        {
            result.Unauthorized();
            await SetContext(context, result, StatusCodes.Status401Unauthorized);
        }
        catch (ForbiddenException)
        {
            result.Forbidden();
            await SetContext(context, result, StatusCodes.Status403Forbidden);
        }
        catch (NotFoundException)
        {
            result.NotFound();
            await SetContext(context, result, StatusCodes.Status404NotFound);
        }
        catch (MethodNotAllowedException)
        {
            result.MethodNotAllowed();
            await SetContext(context, result, StatusCodes.Status405MethodNotAllowed);
        }
        catch (TooManyRequestException)
        {
            result.TooManyRequest();
            await SetContext(context, result, StatusCodes.Status429TooManyRequests);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);

            result.InternalServerError();
            await SetContext(context, result, StatusCodes.Status500InternalServerError);
        }
    }

    private static async Task SetContext(HttpContext context, BaseResult result, int statusCode)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonSerializer.Serialize(result));
    }
}
