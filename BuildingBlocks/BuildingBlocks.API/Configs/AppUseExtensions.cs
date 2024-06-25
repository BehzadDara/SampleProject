using BuildingBlocks.Application;
using BuildingBlocks.Application.Middlewares;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace BuildingBlocks.API.Configs;

public static class AppUseExtensions
{
    public static IApplicationBuilder BaseAppUse(this IApplicationBuilder app)
    {
        app
            .UsingEndpoints()
            .UsingMiddlewares()
            .UsingCors()
            .UsingSwagger()
            .UsingAuthorization();

        return app;
    }

    public static IApplicationBuilder UsingEndpoints(this IApplicationBuilder app)
    {
        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHealthChecks("/healthz", new HealthCheckOptions { ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse });
        });

        return app;
    }

    public static IApplicationBuilder UsingMiddlewares(this IApplicationBuilder app)
    {
        app.UseMiddleware<GlobalExceptionHandler>();
        //app.UseMiddleware<RateLimitMiddleware>();
        app.UseMiddleware<HttpResponseMiddleware>();

        return app;
    }

    public static IApplicationBuilder UsingCors(this IApplicationBuilder app)
    {
        app.UseCors("allowall");

        return app;
    }

    public static IApplicationBuilder UsingSwagger(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        return app;
    }

    public static IApplicationBuilder UsingAuthorization(this IApplicationBuilder app)
    {
        app.UseAuthorization();

        return app;
    }
}
