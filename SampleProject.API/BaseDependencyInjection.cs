using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SampleProject.API.BaseOperationFilters;
using SampleProject.Application.BaseFeatures;
using SampleProject.Domain.BaseInterfaces;
using System.Text;
using System.Text.Json.Serialization;

namespace SampleProject.API;

public static class BaseDependencyInjection
{
    public static IServiceCollection BaseRegister(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .RegisterControllers()
            .RegisterMediatR()
            .RegisterValidator()
            .RegisterAuthentication(configuration)
            .RegisterCurrentUser()
            .RegisterSwagger()
            .RegisterCors();

        return services;
    }

    private static IServiceCollection RegisterControllers(this IServiceCollection services)
    {
        services
            .AddControllers()
            .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

        services.AddEndpointsApiExplorer();

        return services;
    }

    private static IServiceCollection RegisterMediatR(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(BaseResult<>)));

        return services;
    }

    private static IServiceCollection RegisterValidator(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining(typeof(BaseResult<>)); 

        return services;
    }

    private static IServiceCollection RegisterAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"] ?? "AlternativeKey"))
            };
        });

        return services;
    }

    private static IServiceCollection RegisterCurrentUser(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddSingleton<ICurrentUser, CurrentUser>();

        return services;
    }

    private static IServiceCollection RegisterSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Sample Project",
                Version = "v1"
            });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please insert JWT into field",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
            //c.OperationFilter<EnumOperationFilter>();
            //c.OperationFilter<SecurityRequirementsOperationFilter>();
        });
        //services.AddSwaggerGenNewtonsoftSupport();

        return services;
    }

    private static IServiceCollection RegisterCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("allowall", policy =>
            {
                policy
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
            });
        });

        return services;
    }
}
