using BuildingBlocks.Application.Exceptions;
using BuildingBlocks.Application.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace BuildingBlocks.Application;

public class GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger)
{
    public async Task Invoke(HttpContext context)
    {
        var result = new Result();

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
        catch (ForbiddenException ex)
        {
            result.Forbidden(ex.Error);
            await SetContext(context, result, StatusCodes.Status403Forbidden);
        }
        catch (NotFoundException ex)
        {
            result.NotFound(ex.Error);
            await SetContext(context, result, StatusCodes.Status404NotFound);
        }
        catch (MethodNotAllowedException)
        {
            result.MethodNotAllowed();
            await SetContext(context, result, StatusCodes.Status405MethodNotAllowed);
        }
        catch (ConflictException ex)
        {
            result.Conflict(ex.Error);
            await SetContext(context, result, StatusCodes.Status409Conflict);
        }
        catch (TooManyRequestException ex)
        {
            result.TooManyRequest(ex.Error);
            await SetContext(context, result, StatusCodes.Status429TooManyRequests);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);

            result.InternalServerError();
            await SetContext(context, result, StatusCodes.Status500InternalServerError);
        }
    }

    private static async Task SetContext(HttpContext context, Result result, int statusCode)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonSerializer.Serialize(result, GetOptions()));
    }

    private static JsonSerializerOptions GetOptions()
    {
        return new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };
    }
}
