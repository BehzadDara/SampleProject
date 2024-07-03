using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using BuildingBlocks.API.Controllers;
using SampleProject.Application.Features.Authentication.Login;
using Swashbuckle.AspNetCore.Filters;

namespace SampleProject.API.Controllers;

[SwaggerTag("سرویس احراز هویت کاربر")]
public class AuthenticationController(IMediator mediator) : BaseController
{
    [HttpPost]
    [SwaggerOperation("دریافت توکن", "     دریافت توکن برای ورود به سامانه با ورودی های نام کاربری و رمز عبور")]
    [SwaggerRequestExample(typeof(LoginCommand), typeof(LoginCommandExample))]
    [SwaggerResponse(StatusCodes.Status200OK, "احراز هویت با موفقیت انجام شد", typeof(string))]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(TokenExample))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "کاربر یافت نشد", typeof(void))]
    public async Task<IActionResult> Login(LoginCommand request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);
        return ApiResult(result);
    }
}