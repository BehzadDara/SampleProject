using Asp.Versioning;
using BuildingBlocks.Domain.Interfaces;
using BuildingBlocks.Infrastructure.Implementations;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.FeatureManagement;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;
using StackExchange.Redis;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

namespace BuildingBlocks.API.Configs;

public static class DependencyInjection
{
    public static IServiceCollection BaseRegister(this IServiceCollection services,
        IConfiguration configuration, IHostBuilder hostBuilder)
    {
        services
            .RegisterControllers()
            .RegisterAPIVersioning()
            .RegisterLog(configuration, hostBuilder)
            .RegisterMemoryCache()
            .RegisterRedis(configuration)
            .RegisterAuthentication(configuration)
            .RegisterCurrentUser()
            .RegisterSwagger()
            .RegisterCors()
            .RegisterHealthcheck()
            .RegisterLocalization()
            .RegisterFeatureManagement()
            .RegisterHangfire();

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

    public static IServiceCollection RegisterAPIVersioning(this IServiceCollection services)
    {
        services

            .AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader(),
                    new HeaderApiVersionReader("X-API-Version"));
            })

            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });

        return services;
    }

    public static IServiceCollection RegisterLog(this IServiceCollection services,
        IConfiguration configuration, IHostBuilder hostBuilder)
    {
        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Information)
            .WriteTo.File("../Logs/log.txt", restrictedToMinimumLevel: LogEventLevel.Information, rollingInterval: RollingInterval.Day)
            .WriteTo.File("../Logs/logError.txt", restrictedToMinimumLevel: LogEventLevel.Error, rollingInterval: RollingInterval.Day)
            .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(configuration["ElasticSearch:Uri"]!))
            {
                AutoRegisterTemplate = true,
                IndexFormat =
                    $"{Assembly.GetExecutingAssembly().GetName().Name!.ToLower().Replace('.', '-')}-" +
                    $"{DateTime.UtcNow:yyyy-MM}",
                NumberOfReplicas = 2,
                NumberOfShards = 1
            })
            .CreateLogger();

        services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.AddSerilog(dispose: true);
        });

        //hostBuilder.UseSerilog();

        return services;
    }

    public static IServiceCollection RegisterMemoryCache(this IServiceCollection services)
    {
        services.AddMemoryCache();

        return services;
    }

    public static IServiceCollection RegisterRedis(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IConnectionMultiplexer>(provider =>
        {
            var cfg = configuration.GetConnectionString("RedisConnection");
            return ConnectionMultiplexer.Connect(cfg!);
        });

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
                Title = "Project Swagger",
                Version = "v1",
                Description = Resources.ConstantTexts.SwaggerDescription,
                Contact = new OpenApiContact
                {
                    Name = "Behzad Dara",
                    Email = "Behzad.Dara.99@gmail.com",
                    Url = new Uri("https://www.linkedin.com/in/behzaddara/")
                }
            });

            /*c.SwaggerDoc("v2", new OpenApiInfo
            {
                Title = "Project Swagger",
                Version = "v2",
                Description = Resources.ConstantTexts.SwaggerDescription,
                Contact = new OpenApiContact
                {
                    Name = "Behzad Dara",
                    Email = "Behzad.Dara.99@gmail.com",
                    Url = new Uri("https://www.linkedin.com/in/behzaddara/")
                }
            });*/

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please insert JWT into field",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });

            /*c.DocInclusionPredicate((version, apiDesc) =>
            {
                var apiVersionAttributes = apiDesc.ActionDescriptor.EndpointMetadata.OfType<ApiVersionAttribute>();

                if (apiVersionAttributes.Any())
                {
                    return apiDesc.GroupName == version;
                }

                return true;
            });*/

            c.OperationFilter<SecurityRequirementsOperationFilter>();

            c.OperationFilter<AddAcceptLanguageHeaderParameter>();

            c.ExampleFilters();

            c.EnableAnnotations();
        });

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

    public static IServiceCollection RegisterHealthcheck(this IServiceCollection services)
    {
        services.AddHealthChecks()
            .AddDbContextCheck<DBContext>("Database HealthCheck")
            .AddCheck<CustomHealthCheck>("Custom HealthCheck");

        return services;
    }

    public static IServiceCollection RegisterLocalization(this IServiceCollection services)
    {
        services.AddLocalization();

        return services;
    }

    public static IServiceCollection RegisterFeatureManagement(this IServiceCollection services)
    {
        services.AddFeatureManagement();

        return services;
    }

    public static IServiceCollection RegisterHangfire(this IServiceCollection services)
    {
        services.AddHangfire(configuration => configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseDefaultTypeSerializer()
            .UseMemoryStorage());

        services.AddHangfireServer();

        return services;
    }
}