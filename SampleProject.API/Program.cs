using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SampleProject.Application.BaseBehaviors;
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
using SampleProject.Domain.Interfaces;
using SampleProject.Infrastructure;
using SampleProject.Infrastructure.Implementations;
using SampleProject.Infrastructure.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
builder.Services.AddScoped(typeof(ISampleModelRepository), typeof(SampleModelRepository));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddTransient<IRequestHandler<GetGenderEnumQuery, BaseResult<IList<EnumViewModel>>>, GetGenderEnumQueryHandler>();
builder.Services.AddTransient<IRequestHandler<GetAllSampleModelsQuery, BaseResult<IList<SampleModelViewModel>>>, GetAllSampleModelsQueryHandler>();
builder.Services.AddTransient<IRequestHandler<GetSampleModelByIdQuery, BaseResult<SampleModelViewModel>>, GetSampleModelByIdQueryHandler>();
builder.Services.AddTransient<IRequestHandler<GetSampleModelsByFilterQuery, BaseResult<PagedList<SampleModelViewModel>>>, GetSampleModelsByFilterQueryHandler>();
builder.Services.AddTransient<IRequestHandler<CreateSampleModelCommand, BaseResult>, CreateSampleModelCommandHandler>();
builder.Services.AddTransient<IRequestHandler<UpdateSampleModelCommand, BaseResult>, UpdateSampleModelCommandHandler>();
builder.Services.AddTransient<IRequestHandler<DeleteSampleModelCommand, BaseResult>, DeleteSampleModelCommandHandler>();

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services.AddTransient<IValidator<CreateSampleModelCommand>, CreateSampleModelValidator>();
builder.Services.AddTransient<IValidator<UpdateSampleModelCommand>, UpdateSampleModelValidator>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<SampleProjectDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SampleProjectConnection")),
    ServiceLifetime.Scoped);

builder.Services.AddScoped<BaseDBContext>(provider => provider.GetService<SampleProjectDBContext>()!);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

var app = builder.Build();

app.UseCors("allowall");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();