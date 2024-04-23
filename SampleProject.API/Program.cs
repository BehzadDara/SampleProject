using MediatR;
using Microsoft.EntityFrameworkCore;
using SampleProject.Application;
using SampleProject.Application.BaseFeature;
using SampleProject.Application.BaseViewModels;
using SampleProject.Application.Features.SampleModel.Queries.GetGenderEnum;
using SampleProject.Domain.BaseInterfaces;
using SampleProject.Infrastructure;
using SampleProject.Infrastructure.Implementations;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddMediatR(typeof(Program).GetTypeInfo().Assembly);

builder.Services.AddTransient<IRequestHandler<GetGenderEnumQuery, BaseResult<IList<EnumViewModel>>>, GetGenderEnumQueryHandler>();
//builder.Services.AddTransient<IBaseCommandQueryHandler<IBaseCommandQuery<BaseResult>>>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<SampleProjectDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SampleProjectConnection")),
    ServiceLifetime.Scoped);

builder.Services.AddScoped<BaseDBContext>(provider => provider.GetService<SampleProjectDBContext>()!);

builder.Services.AddScoped(typeof(IBaseUnitOfWork), typeof(BaseUnitOfWork));
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

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