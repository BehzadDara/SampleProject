using BuildingBlocks.Application.Attributes;
using BuildingBlocks.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Cryptography;
using System.Text;

namespace BuildingBlocks.Application.Middlewares;

public class IdempotentMiddleware(RequestDelegate next, IMemoryCache cache, ICurrentUser currentUser)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var endpoint = context.GetEndpoint();
        if (endpoint?.Metadata.GetMetadata<IdempotentAttribute>() is not null)
        {
            string cacheKey = await GenerateCacheKeyAsync(context);

            if (cache.TryGetValue(cacheKey, out CachedResponse? cachedResponse))
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = cachedResponse!.StatusCode;
                await context.Response.WriteAsync(cachedResponse.Content);
                return;
            }

            var originalResponseBody = context.Response.Body;
            using var newResponseBody = new MemoryStream();
            context.Response.Body = newResponseBody;

            await next(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var responseText = new StreamReader(context.Response.Body).ReadToEnd();
            context.Response.Body.Seek(0, SeekOrigin.Begin);

            var cacheEntry = new CachedResponse
            {
                StatusCode = context.Response.StatusCode,
                Content = responseText
            };

            cache.Set(cacheKey, cacheEntry, TimeSpan.FromSeconds(5));

            await newResponseBody.CopyToAsync(originalResponseBody);
        }
        else
        {
            await next(context);
        }
    }

    private async Task<string> GenerateCacheKeyAsync(HttpContext context)
    {
        var request = context.Request;
        var keyBuilder = new StringBuilder();

        keyBuilder.Append(currentUser.IPAddress);
        keyBuilder.Append(request.Path);
        keyBuilder.Append(request.Method);
        keyBuilder.Append(request.QueryString);

        context.Request.EnableBuffering();
        using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true))
        {
            var bodyString = await reader.ReadToEndAsync();
            keyBuilder.Append(bodyString);
            context.Request.Body.Position = 0;
        }

        var hash = MD5.HashData(Encoding.UTF8.GetBytes(keyBuilder.ToString()));
        return Convert.ToBase64String(hash);
    }
}

public class CachedResponse
{
    public int StatusCode { get; set; }
    public string Content { get; set; } = string.Empty;
}