using SampleProject.Domain.Interfaces;
using SampleProject.Infrastructure.Repositories;
using SampleProject.Infrastructure;
using SampleProject.Infrastructure.Implementations;
using Microsoft.EntityFrameworkCore;

namespace SampleProject.API.Configs;

public static class DependencyInjection
{
    public static IServiceCollection Register(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .RegisterRepositories()
            .RegisterDBContext(configuration)
            .RegisterAuthentication();

        return services;
    }

    private static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
        services.AddScoped(typeof(ISampleModelRepository), typeof(SampleModelRepository));
        services.AddScoped(typeof(IAnotherSampleModelRepository), typeof(AnotherSampleModelRepository));

        return services;
    }

    private static IServiceCollection RegisterDBContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SampleProjectDBContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("SampleProjectConnection")),
            ServiceLifetime.Scoped);
        services.AddScoped<BaseDBContext>(provider => provider.GetService<SampleProjectDBContext>()!);

        services.AddDbContext<AnotherSampleProjectDBContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("AnotherSampleProjectConnection")),
            ServiceLifetime.Scoped);
        services.AddScoped<BaseDBContext>(provider => provider.GetService<AnotherSampleProjectDBContext>()!);

        return services;
    }

    private static IServiceCollection RegisterAuthentication(this IServiceCollection services)
    {
        services.AddAuthorizationBuilder()
            .AddPolicy("CanDeletePolicy", policy =>
                policy.RequireClaim("Permissions", "CanDelete"));

        return services;
    }
}
