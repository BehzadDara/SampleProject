using SampleProject.API;

var builder = WebApplication.CreateBuilder(args);

builder.Services.BaseRegister(builder.Configuration).Register(builder.Configuration);

var app = builder.Build();

app.BaseAppUse().AppUse();

app.MapControllers();

app.Run();