using SampleProject.Domain.Interfaces;
using SampleProject.Infrastructure.Repositories;
using SampleProject.Infrastructure;
using SampleProject.Application.Features.SampleModel.Commands.CreateSampleModel;
using SampleProject.Application.Features.SampleModel.Commands.UpdateSampleModel;
using FluentValidation;
using SampleProject.Infrastructure.Implementations;
using Microsoft.EntityFrameworkCore;

namespace SampleProject.API;

public static class DependencyInjection
{
    public static IServiceCollection Register(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .RegisterRepositories()
            .RegisterValidators()
            .RegisterDBContext(configuration)
            .RegisterAuthentication();

        return services;
    }

    private static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
        services.AddScoped(typeof(ISampleModelRepository), typeof(SampleModelRepository));

        return services;
    }

    private static IServiceCollection RegisterValidators(this IServiceCollection services)
    {
        services.AddTransient<IValidator<CreateSampleModelCommand>, CreateSampleModelValidator>();
        services.AddTransient<IValidator<UpdateSampleModelCommand>, UpdateSampleModelValidator>();

        return services;
    }

    private static IServiceCollection RegisterDBContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SampleProjectDBContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("SampleProjectConnection")),
            ServiceLifetime.Scoped);
        services.AddScoped<BaseDBContext>(provider => provider.GetService<SampleProjectDBContext>()!);

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
