using BuildingBlocks.API.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;
using SampleProject.Application.Features.AnotherSampleModel.Queries.GetAnotherSampleModelTotalCount;
using Swashbuckle.AspNetCore.Annotations;

namespace SampleProject.API.Controllers;

[SwaggerTag("AnotherSampleModel Service")]
public class AnotherSampleModelController(IMediator mediator, IFeatureManager featureManager) : BaseController
{
    [HttpGet]
    public IActionResult RedirectTest()
    {
        return Redirect("https://www.google.com/");
    }

    [HttpGet]
    [SwaggerOperation("Get Total Count")]
    [SwaggerResponse(StatusCodes.Status200OK, "Retrieved", typeof(int))]
    [SwaggerResponse(StatusCodes.Status501NotImplemented, "Not implemented", typeof(void))]
    public async Task<IActionResult> GetTotalCount(CancellationToken cancellationToken)
    {
        if (!await featureManager.IsEnabledAsync("AnotherSampleModelGetTotalCountFeature"))
            throw new BuildingBlocks.Application.Exceptions.NotImplementedException(BuildingBlocks.Resources.Messages.NotImplemented);

        var result = await mediator.Send(new GetAnotherSampleModelTotalCountQuery(), cancellationToken);
        return ApiResult(result);
    }
}