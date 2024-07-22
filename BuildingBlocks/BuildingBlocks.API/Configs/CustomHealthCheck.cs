using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BuildingBlocks.API.Configs;

internal class CustomHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var random = new Random();
        var expectedNumber = random.Next(60, 100);
        var actualNumber = random.Next(0, 100);

        var data = new Dictionary<string, object>
        {
            { nameof(expectedNumber), expectedNumber },
            { nameof(actualNumber), actualNumber }
        };

        var status = actualNumber switch
        {
            >= 0 and < 30 => HealthStatus.Unhealthy,
            >= 30 and < 60 => HealthStatus.Degraded,
            _ => HealthStatus.Healthy,
        };

        var result = new HealthCheckResult(status, null, null, data);
        return Task.FromResult(result);
    }
}
