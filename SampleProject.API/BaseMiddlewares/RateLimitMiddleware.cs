using Microsoft.Extensions.Caching.Memory;
using SampleProject.Application.BaseFeatures;
using SampleProject.Domain.BaseInterfaces;
using System.Text.Json;

namespace SampleProject.API.BaseMiddlewares;

public class RateLimitMiddleware(RequestDelegate next, IMemoryCache memoryCache, ICurrentUser currentUser)
{
    private readonly TimeSpan timeLimit = TimeSpan.FromMinutes(1);
    private readonly int countLimit = 10;

    public async Task Invoke(HttpContext context)
    {
        var key = currentUser.IPAddress;

        memoryCache.TryGetValue(key, out int requestCount);
        
        if (requestCount > countLimit)
        {
            var result = new BaseResult();
            result.TooManyRequest();

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status429TooManyRequests;

            await context.Response.WriteAsync(JsonSerializer.Serialize(result));
        }
        else
        {
            await next(context);
        }

        requestCount++;
        memoryCache.Set(key, requestCount, timeLimit);
    }
}