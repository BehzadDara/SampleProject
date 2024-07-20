using BuildingBlocks.Application.Jobs;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using SampleProject.Infrastructure;

namespace SampleProject.API.Configs;

public static class AppUseExtensions
{
    public static IApplicationBuilder AppUse(this IApplicationBuilder app, IConfiguration configuration)
    {
        app.MigratingDatabase();

        UsingJobs(configuration);

        return app;
    }

    private static void UsingJobs(IConfiguration configuration)
    {
        RecurringJob.AddOrUpdate<HealthCheckJob>("SampleJob", x => HealthCheckJob.CheckStatus(configuration["HealthCheck:Uri"]!), "* * * * *");
    }

    private static IApplicationBuilder MigratingDatabase(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var context = serviceScope.ServiceProvider.GetService<SampleProjectDBContext>();
        context?.Database.Migrate();

        return app;
    }
}
