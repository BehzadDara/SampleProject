using BuildingBlocks.Application.Jobs;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using SampleProject.Infrastructure;

namespace SampleProject.API.Configs;

public static class AppUseExtensions
{
    public static IApplicationBuilder AppUse(this IApplicationBuilder app)
    {
        UsingJobs();

        app.MigratingDatabase();

        return app;
    }

    private static void UsingJobs()
    {
        RecurringJob.AddOrUpdate<HealthCheckJob>("SampleJob", x => HealthCheckJob.CheckStatus(), "*/10 * * * * *");
    }

    private static IApplicationBuilder MigratingDatabase(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var context = serviceScope.ServiceProvider.GetService<SampleProjectDBContext>();
        context?.Database.Migrate();

        return app;
    }
}
