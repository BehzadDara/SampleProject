using MediatR;
using Microsoft.AspNetCore.Mvc;
using SampleProject.Application.BaseFeatures;

namespace SampleProject.API.BaseControllers;

[ApiController]
[Route("[controller]/[action]")]
public abstract class BaseController : ControllerBase
{
    protected IActionResult BaseApiResult(BaseResult result)
    {
        return result.StatusCode switch
        {
            StatusCodes.Status200OK => Ok(result),
            StatusCodes.Status400BadRequest => BadRequest(result),
            StatusCodes.Status401Unauthorized => Unauthorized(result),
            StatusCodes.Status404NotFound => NotFound(result),
            _ => throw new Exception()
        };
    }
}
