using BuildingBlocks.Application;
using BuildingBlocks.Application.Middlewares;
using BuildingBlocks.Domain.Enums;
using Hangfire;
using Hangfire.Dashboard;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Prometheus;

namespace BuildingBlocks.API.Configs;

public static class AppUseExtensions
{
    public static IApplicationBuilder BaseAppUse(this IApplicationBuilder app)
    {
        app
            .UsingLocalization()
            .UsingCors()
            .UsingSwagger()
            .UsingHangfire()
            .UsingMetrics()
            .UsingRouting()
            .UsingAuthorization()
            .UsingMiddlewares()
            .UsingEndpoints();

        return app;
    }

    public static IApplicationBuilder UsingMiddlewares(this IApplicationBuilder app)
    {
        app.UseMiddleware<IdempotentMiddleware>();
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
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
            //options.SwaggerEndpoint("/swagger/v2/swagger.json", "API V2");
        });

        return app;
    }

    public static IApplicationBuilder UsingAuthorization(this IApplicationBuilder app)
    {
        app.UseAuthorization();

        return app;
    }

    public static IApplicationBuilder UsingLocalization(this IApplicationBuilder app)
    {
        var supportedCultures = Enum
            .GetValues(typeof(Languages))
            .Cast<Languages>()
            .Select(x => x.ToString())
            .ToArray();

        var localizationOptions = new RequestLocalizationOptions()
            .SetDefaultCulture(supportedCultures[0])
            .AddSupportedCultures(supportedCultures)
            .AddSupportedUICultures(supportedCultures);

        app.UseRequestLocalization(localizationOptions);

        return app;
    }

    public static IApplicationBuilder UsingHangfire(this IApplicationBuilder app)
    {
        app.UseHangfireDashboard("/hangfire", new DashboardOptions
        {
            Authorization = new[] { new DashboardNoAuthorizationFilter() }
        });

        return app;
    }

    public static IApplicationBuilder UsingMetrics(this IApplicationBuilder app)
    {
        app.UseHttpMetrics();

        return app;
    }

    public static IApplicationBuilder UsingRouting(this IApplicationBuilder app)
    {
        app.UseRouting();

        return app;
    }

    public static IApplicationBuilder UsingEndpoints(this IApplicationBuilder app)
    {
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHealthChecks("/healthz", new HealthCheckOptions { ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse });
            endpoints.MapMetrics("/metrics");
        });

        return app;
    }
}

public class DashboardNoAuthorizationFilter : IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext dashboardContext)
    {
        return true;
    }
}
