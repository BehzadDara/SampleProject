using BuildingBlocks.Application.Jobs;
using Hangfire;

namespace SampleProject.API.Configs;

public static class AppUseExtensions
{
    public static IApplicationBuilder AppUse(this IApplicationBuilder app)
    {
        UsingJobs();

        return app;
    }

    private static void UsingJobs()
    {
        RecurringJob.AddOrUpdate<HealthCheckJob>("SampleJob", x => HealthCheckJob.CheckStatus(), "*/10 * * * * *");
    }
}
