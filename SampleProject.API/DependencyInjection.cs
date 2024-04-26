using SampleProject.Domain.Interfaces;
using SampleProject.Infrastructure.Repositories;
using SampleProject.Infrastructure;
using MediatR;
using SampleProject.Application.BaseFeatures.Authentication.Login;
using SampleProject.Application.BaseFeatures;
using SampleProject.Application.BaseViewModels;
using SampleProject.Application.Features.SampleModel.Commands.CreateSampleModel;
using SampleProject.Application.Features.SampleModel.Commands.DeleteSampleModel;
using SampleProject.Application.Features.SampleModel.Commands.UpdateSampleModel;
using SampleProject.Application.Features.SampleModel.Queries.GetAllSampleModels;
using SampleProject.Application.Features.SampleModel.Queries.GetGenderEnum;
using SampleProject.Application.Features.SampleModel.Queries.GetSampleModelById;
using SampleProject.Application.Features.SampleModel.Queries.GetSampleModelsByFilter;
using SampleProject.Application.ViewModels;
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
            .RegisterHandlers()
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

    private static IServiceCollection RegisterHandlers(this IServiceCollection services)
    {
        services.AddTransient<IRequestHandler<LoginCommand, BaseResult<string>>, LoginCommandHandler>();
        services.AddTransient<IRequestHandler<GetGenderEnumQuery, BaseResult<IList<EnumViewModel>>>, GetGenderEnumQueryHandler>();
        services.AddTransient<IRequestHandler<GetAllSampleModelsQuery, BaseResult<IList<SampleModelViewModel>>>, GetAllSampleModelsQueryHandler>();
        services.AddTransient<IRequestHandler<GetSampleModelByIdQuery, BaseResult<SampleModelViewModel>>, GetSampleModelByIdQueryHandler>();
        services.AddTransient<IRequestHandler<GetSampleModelsByFilterQuery, BaseResult<PagedList<SampleModelViewModel>>>, GetSampleModelsByFilterQueryHandler>();
        services.AddTransient<IRequestHandler<CreateSampleModelCommand, BaseResult>, CreateSampleModelCommandHandler>();
        services.AddTransient<IRequestHandler<UpdateSampleModelCommand, BaseResult>, UpdateSampleModelCommandHandler>();
        services.AddTransient<IRequestHandler<DeleteSampleModelCommand, BaseResult>, DeleteSampleModelCommandHandler>();

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
