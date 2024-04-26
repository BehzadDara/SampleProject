using SampleProject.API.BaseMiddlewares;
using SampleProject.Application.Features.SampleModel.Commands.CreateSampleModel;
using SampleProject.Application.Features.SampleModel.Commands.UpdateSampleModel;

namespace SampleProject.API;

public static class AppUseExtensions
{
    public static IApplicationBuilder AppUse(this IApplicationBuilder app)
    {
        app
            .UsingBadRequestMiddleware();

        return app;
    }

    public static IApplicationBuilder UsingBadRequestMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<BadRequestMiddleware<CreateSampleModelCommand>>();
        app.UseMiddleware<BadRequestMiddleware<UpdateSampleModelCommand>>();

        return app;
    }
}
