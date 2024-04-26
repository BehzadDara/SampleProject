using FluentValidation;
using SampleProject.Application.BaseFeatures;
using System.Text.Json;

namespace SampleProject.API.BaseMiddlewares;

public class BadRequestMiddleware(RequestDelegate next, IServiceProvider serviceProvider) //where TRequest : class
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            var request = await context.Request.ReadFromJsonAsync<IBaseCommandQuery>()
                ?? throw new Exception();

            var validator = serviceProvider.GetService<IValidator<IBaseCommandQuery>>() 
                ?? throw new Exception();

            var validationResult = await validator.ValidateAsync(request);
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