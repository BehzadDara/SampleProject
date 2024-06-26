﻿using BuildingBlocks.API.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SampleProject.Application.Features.AnotherSampleModel.Queries.GetAnotherSampleModelTotalCount;
using Swashbuckle.AspNetCore.Annotations;

namespace SampleProject.API.Controllers;

[SwaggerTag("AnotherSampleModel Service")]
public class AnotherSampleModelController(IMediator mediator) : BaseController
{
    [HttpGet]
    public IActionResult RedirectTest()
    {
        return Redirect("https://app.keepa.ir/");
    }

    [HttpGet]
    [SwaggerOperation("Get Total Count")]
    [SwaggerResponse(StatusCodes.Status200OK, "Retrieved", typeof(int))]
    public async Task<IActionResult> GetTotalCount(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAnotherSampleModelTotalCountQuery(), cancellationToken);
        return ApiResult(result);
    }
}