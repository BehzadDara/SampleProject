using AnotherSampleProject.API;
using AnotherSampleProject.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "AnotherProject Swagger",
        Contact = new OpenApiContact
        {
            Name = "Behzad Dara",
            Email = "Behzad.Dara.99@gmail.com",
            Url = new Uri("https://www.linkedin.com/in/behzaddara/")
        }
    });
});

builder.Services.AddDbContext<AnotherSampleProjectDBContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("AnotherSampleProjectConnection")));


builder.Services.AddScoped<ITestModelService, TestModelService>();

var app = builder.Build();

MigrateDatabase(app);

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/All", async (ITestModelService testService, CancellationToken cancellationToken) =>
{
    var items = await testService.GetAll(cancellationToken);
    return Results.Ok(items);
});

app.MapPost("/Add", async (ITestModelService testService, CancellationToken cancellationToken, [FromBody] string name) =>
{
    await testService.Add(name, cancellationToken);
    return Results.Ok();
});

app.Run();

static void MigrateDatabase(IApplicationBuilder app)
{
    using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
    var context = serviceScope.ServiceProvider.GetService<AnotherSampleProjectDBContext>();
    context?.Database.Migrate();
}