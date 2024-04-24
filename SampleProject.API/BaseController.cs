using MediatR;
using Microsoft.AspNetCore.Mvc;
using SampleProject.Application.BaseFeatures;
using SampleProject.Application.BaseViewModels;
using SampleProject.Application.Features.SampleModel.Queries.GetGenderEnum;

namespace SampleProject.API;

[ApiController]
[Route("[controller]/[action]")]
public abstract class BaseController : ControllerBase
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

    protected IActionResult BaseApiResult(BaseResult result)
    {
        return result.StatusCode switch
        {
            StatusCodes.Status200OK => Ok(result),
            StatusCodes.Status400BadRequest => BadRequest(result),
            StatusCodes.Status404NotFound => NotFound(result),
            _ => throw new Exception()
        };
    }
}
