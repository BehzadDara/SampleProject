using Microsoft.AspNetCore.Mvc;
using BuildingBlocks.Application.Features;

namespace BuildingBlocks.API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public abstract class BaseController : ControllerBase
{
    protected IActionResult ApiResult(Result result)
    {
        return Ok(result);
    }
}
