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
            result.BadRequest(ex.Error, ex.Errors);
            await SetContext(context, result, StatusCodes.Status400BadRequest, ex);
        }
        catch (UnauthorizedException ex)
        {
            result.Unauthorized(ex.Error);
            await SetContext(context, result, StatusCodes.Status401Unauthorized, ex);
        }
        catch (ForbiddenException ex)
        {
            result.Forbidden(ex.Error);
            await SetContext(context, result, StatusCodes.Status403Forbidden, ex);
        }
        catch (NotFoundException ex)
        {
            result.NotFound(ex.Error);
            await SetContext(context, result, StatusCodes.Status404NotFound, ex);
        }
        catch (MethodNotAllowedException ex)
        {
            result.MethodNotAllowed(ex.Error);
            await SetContext(context, result, StatusCodes.Status405MethodNotAllowed, ex);
        }
        catch (ConflictException ex)
        {
            result.Conflict(ex.Error);
            await SetContext(context, result, StatusCodes.Status409Conflict, ex);
        }
        catch (TooManyRequestException ex)
        {
            result.TooManyRequest(ex.Error);
            await SetContext(context, result, StatusCodes.Status429TooManyRequests, ex);
        }
        catch (Exceptions.NotImplementedException ex)
        {
            result.NotImplemented(ex.Error);
            await SetContext(context, result, StatusCodes.Status501NotImplemented, ex);
        }
        catch (Exception ex)
        {
            //logger.LogError(ex.Message);

            result.InternalServerError(ex.Message);
            await SetContext(context, result, StatusCodes.Status500InternalServerError, ex);
        }
    }

    private async Task SetContext(HttpContext context, Result result, int statusCode, Exception ex)
    {
        logger.LogError(
            "Error in GlobalExceptionHandler " +
            "at {datetime}, " +
            "with status code {statusCode} " +
            "and exception {exception}"
            , DateTime.Now, statusCode, ex);

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
