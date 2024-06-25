using BuildingBlocks.Application.Exceptions;
using BuildingBlocks.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;

namespace BuildingBlocks.Application.Middlewares;

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
            throw new TooManyRequestException(Resources.Messages.TooManyRequest);
        }
        else
        {
            await next(context);
        }

        requestCount++;
        memoryCache.Set(key, requestCount, timeLimit);
    }
}