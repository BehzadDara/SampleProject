using MediatR;
using Microsoft.AspNetCore.Mvc;
using SampleProject.Application.BaseFeatures.Authentication.Login;

namespace SampleProject.API.BaseControllers;

public class AuthenticationController(IMediator mediator) : BaseController
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Login(LoginCommand request)
    {

        var result = await mediator.Send(request);
        return BaseApiResult(result);
    }
}
