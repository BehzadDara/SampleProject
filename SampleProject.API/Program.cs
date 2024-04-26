using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SampleProject.API;
using SampleProject.API.BaseMiddlewares;
using SampleProject.Application.BaseBehaviors;
using SampleProject.Application.BaseFeatures;
using SampleProject.Application.BaseFeatures.Authentication.Login;
using SampleProject.Application.BaseViewModels;
using SampleProject.Application.Features.SampleModel.Commands.CreateSampleModel;
using SampleProject.Application.Features.SampleModel.Commands.DeleteSampleModel;
using SampleProject.Application.Features.SampleModel.Commands.UpdateSampleModel;
using SampleProject.Application.Features.SampleModel.Queries.GetAllSampleModels;
using SampleProject.Application.Features.SampleModel.Queries.GetGenderEnum;
using SampleProject.Application.Features.SampleModel.Queries.GetSampleModelById;
using SampleProject.Application.Features.SampleModel.Queries.GetSampleModelsByFilter;
using SampleProject.Application.ViewModels;
using SampleProject.Domain.BaseInterfaces;
using SampleProject.Domain.Interfaces;
using SampleProject.Infrastructure;
using SampleProject.Infrastructure.Implementations;
using SampleProject.Infrastructure.Repositories;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
builder.Services.AddScoped(typeof(ISampleModelRepository), typeof(SampleModelRepository));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddTransient<IRequestHandler<LoginCommand, BaseResult<string>>, LoginCommandHandler>();

builder.Services.AddTransient<IRequestHandler<GetGenderEnumQuery, BaseResult<IList<EnumViewModel>>>, GetGenderEnumQueryHandler>();
builder.Services.AddTransient<IRequestHandler<GetAllSampleModelsQuery, BaseResult<IList<SampleModelViewModel>>>, GetAllSampleModelsQueryHandler>();
builder.Services.AddTransient<IRequestHandler<GetSampleModelByIdQuery, BaseResult<SampleModelViewModel>>, GetSampleModelByIdQueryHandler>();
builder.Services.AddTransient<IRequestHandler<GetSampleModelsByFilterQuery, BaseResult<PagedList<SampleModelViewModel>>>, GetSampleModelsByFilterQueryHandler>();
builder.Services.AddTransient<IRequestHandler<CreateSampleModelCommand, BaseResult>, CreateSampleModelCommandHandler>();
builder.Services.AddTransient<IRequestHandler<UpdateSampleModelCommand, BaseResult>, UpdateSampleModelCommandHandler>();
builder.Services.AddTransient<IRequestHandler<DeleteSampleModelCommand, BaseResult>, DeleteSampleModelCommandHandler>();

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

//builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));
//builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services.AddTransient<IValidator<CreateSampleModelCommand>, CreateSampleModelValidator>();
builder.Services.AddTransient<IValidator<UpdateSampleModelCommand>, UpdateSampleModelValidator>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? "AlternativeKey"))
    };
});


builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<ICurrentUser, CurrentUser>();

builder.Services.AddDbContext<SampleProjectDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SampleProjectConnection")),
    ServiceLifetime.Scoped);

builder.Services.AddScoped<BaseDBContext>(provider => provider.GetService<SampleProjectDBContext>()!);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
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
    //c.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddCors(options => 
{
    options.AddPolicy("allowall", policy =>
    {
        policy
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("CanDeletePolicy", policy =>
        policy.RequireClaim("Permissions", "CanDelete"));

var app = builder.Build();

app.UseMiddleware<UnauthorizedMiddleware>();
app.UseMiddleware<ForbiddenMiddleware>();
app.UseMiddleware<BadRequestMiddleware<CreateSampleModelCommand>>();
app.UseMiddleware<BadRequestMiddleware<UpdateSampleModelCommand>>();
app.UseMiddleware<InternalServerErrorMiddleware>();

app.UseCors("allowall");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();