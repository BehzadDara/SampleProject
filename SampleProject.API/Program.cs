using BuildingBlocks.API.Configs;
using SampleProject.API.Configs;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.BaseRegister(builder.Configuration, builder.Host).Register(builder.Configuration);
builder.Host.UseSerilog();

var app = builder.Build();

app.BaseAppUse().AppUse(builder.Configuration);

app.Run();