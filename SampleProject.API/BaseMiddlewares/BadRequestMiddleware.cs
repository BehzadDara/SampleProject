using SampleProject.Application.BaseFeatures;
using System.Net;
using System.Text;
using System.Text.Json;

namespace SampleProject.API.BaseMiddlewares;

public class BadRequeskkktMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        await next(context);

        if (context.Response.StatusCode == (int)HttpStatusCode.BadRequest)
        {
            var result = new BaseResult();
            result.Unauthorized();

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

            await context.Response.WriteAsync(JsonSerializer.Serialize(result));
        }
    }
}

public class BadRequestMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        var originalBodyStream = context.Response.Body;

        using var responseBody = new MemoryStream();
        context.Response.Body = responseBody;

        await next(context);

        if (context.Response.StatusCode == StatusCodes.Status400BadRequest)
        {
            responseBody.Seek(0, SeekOrigin.Begin);
            var responseBodyContent = await new StreamReader(responseBody).ReadToEndAsync();

            var validationErrors = ParseValidationErrors(responseBodyContent);
            var jsonResponse = FormatResponse(validationErrors);

            context.Response.ContentType = "application/json";
            context.Response.ContentLength = Encoding.UTF8.GetBytes(jsonResponse).Length;

            responseBody.Seek(0, SeekOrigin.Begin);
            await context.Response.WriteAsync(jsonResponse);
        }
        else
        {
            responseBody.Seek(0, SeekOrigin.Begin);
            await responseBody.CopyToAsync(originalBodyStream);
        }
    }

    private static List<string> ParseValidationErrors(string responseBodyContent)
    {
        var jsonDoc = JsonDocument.Parse(responseBodyContent);
        var errors = jsonDoc.RootElement.GetProperty("errors").EnumerateArray()
            .Select(e => e.GetProperty("errorMessage").GetString())
            .ToList();
        return errors;
    }

    private static string FormatResponse(List<string> validationErrors)
    {
        var jsonResponse = JsonSerializer.Serialize(new { errors = validationErrors });
        return jsonResponse;
    }
}