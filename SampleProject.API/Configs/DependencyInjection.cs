using SampleProject.Domain.Interfaces;
using SampleProject.Infrastructure.Repositories;
using SampleProject.Infrastructure;
using Microsoft.EntityFrameworkCore;
using BuildingBlocks.Infrastructure.Implementations;
using SampleProject.Application;
using FluentValidation;
using BuildingBlocks.Application.Behaviours;
using MediatR;
using Swashbuckle.AspNetCore.Filters;

namespace SampleProject.API.Configs;

public static class DependencyInjection
{
    public static IServiceCollection Register(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .RegisterRepositories()
            .RegisterDBContext(configuration)
            .RegisterAuthentication()
            .RegisterMediatR()
            .RegisterValidator()
            .RegisterSwagger();

        return services;
    }
    private static IServiceCollection RegisterMediatR(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining(typeof(SampleModelMapper));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        });

        return services;
    }

    private static IServiceCollection RegisterValidator(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining(typeof(SampleModelMapper));

        return services;
    }

    private static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(ISampleProjectUnitOfWork), typeof(SampleProjectUnitOfWork));
        services.AddScoped(typeof(ISampleModelRepository), typeof(SampleModelRepository));
        services.AddScoped(typeof(IAnotherSampleModelRepository), typeof(AnotherSampleModelRepository));

        return services;
    }

    private static IServiceCollection RegisterDBContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SampleProjectDBContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")),
            ServiceLifetime.Scoped);
        services.AddScoped<DBContext>(provider => provider.GetService<SampleProjectDBContext>()!);

        return services;
    }

    private static IServiceCollection RegisterAuthentication(this IServiceCollection services)
    {
        services.AddAuthorizationBuilder()
            .AddPolicy("CanDeletePolicy", policy =>
            policy.RequireClaim("Permissions", "CanDelete"));

        return services;
    }

    private static IServiceCollection RegisterSwagger(this IServiceCollection services)
    {
        services.AddSwaggerExamplesFromAssemblyOf(typeof(SampleModelMapper));

        return services;
    }
}
