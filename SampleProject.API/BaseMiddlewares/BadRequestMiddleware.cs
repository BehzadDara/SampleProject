using FluentValidation;
using SampleProject.Application.BaseFeatures;
using System.Text.Json;

namespace SampleProject.API.BaseMiddlewares;

public class BadRequestMiddleware<TRequest>(RequestDelegate next, IValidator<TRequest> validator) where TRequest : class
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            var request = await context.Request.ReadFromJsonAsync<TRequest>();
            var validationResult = await validator.ValidateAsync(request!);
            if (!validationResult.IsValid)
            {
                var result = new BaseResult();
                result.BadRequest(validationResult.Errors);

                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(JsonSerializer.Serialize(result));
                return;
            }
        }
        catch { }

        await next(context);
    }
}