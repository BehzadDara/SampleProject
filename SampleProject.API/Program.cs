using BuildingBlocks.API.Configs;
using SampleProject.API.Configs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.BaseRegister(builder.Configuration).Register(builder.Configuration);

var app = builder.Build();

app.BaseAppUse().AppUse();

app.MapControllers();

app.MapGet("/redirect", () =>
{
    return Results.Redirect("https://app.keepa.ir/", true);
});

app.Run();