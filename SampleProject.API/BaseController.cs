using Microsoft.AspNetCore.Mvc;
using SampleProject.Application.BaseFeature;

namespace SampleProject.API;

[ApiController]
[Route("[controller]/[action]")]
public abstract class BaseController() : ControllerBase
{
    protected string PrivateCurrentUser {  get; private set; } = string.Empty;

    protected string CurrentUser
    {
        get
        {
            PrivateCurrentUser ??= "Test";

            return PrivateCurrentUser;
        }
    }

    protected IActionResult BaseApiResult<T>(BaseResult<T> result)
    {
        return result.StatusCode switch
        {
            StatusCodes.Status200OK => Ok(result),
            StatusCodes.Status404NotFound => NotFound(result),
            _ => throw new Exception()
        };
    }

    protected IActionResult BaseApiResult(BaseResult result)
    {
        return result.StatusCode switch
        {
            StatusCodes.Status200OK => Ok(result),
            StatusCodes.Status404NotFound => NotFound(result),
            _ => throw new Exception()
        };
    }
}
