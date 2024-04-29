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
        return Ok(result);
    }
}
