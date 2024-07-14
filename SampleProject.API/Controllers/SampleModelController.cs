using BuildingBlocks.API.Controllers;
using BuildingBlocks.Application.Attributes;
using BuildingBlocks.Application.Features;
using BuildingBlocks.Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleProject.Application.Features.SampleModel.Commands.CreateSampleModel;
using SampleProject.Application.Features.SampleModel.Commands.DeleteSampleModel;
using SampleProject.Application.Features.SampleModel.Commands.UpdateSampleModel;
using SampleProject.Application.Features.SampleModel.Queries.GetAllSampleModels;
using SampleProject.Application.Features.SampleModel.Queries.GetGenderEnum;
using SampleProject.Application.Features.SampleModel.Queries.GetSampleModelById;
using SampleProject.Application.Features.SampleModel.Queries.GetSampleModelsByFilter;
using SampleProject.Application.Features.SampleModel.Queries.GetSampleModelTotalCount;
using SampleProject.Application.ViewModels;
using Swashbuckle.AspNetCore.Annotations;

namespace SampleProject.API.Controllers;

[SwaggerTag("SampleModel Service")]
public class SampleModelController(IMediator mediator) : BaseController
{
    [HttpGet("{id}")]
    [SwaggerOperation("Get By Id")]
    [SwaggerResponse(StatusCodes.Status200OK, "Retrieved", typeof(SampleModelViewModel))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Not Found", typeof(void))]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetSampleModelByIdQuery(id), cancellationToken);
        return ApiResult(result);
    }

    [HttpGet]
    [SwaggerOperation("Get All")]
    [SwaggerResponse(StatusCodes.Status200OK, "Retrieved", typeof(List<SampleModelViewModel>))]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAllSampleModelsQuery(), cancellationToken);
        return ApiResult(result);
    }

    [HttpGet]
    [SwaggerOperation("Get By Filter")]
    [SwaggerResponse(StatusCodes.Status200OK, "Retrieved", typeof(PagedList<SampleModelViewModel>))]
    public async Task<IActionResult> GetByFilter([FromQuery] GetSampleModelsByFilterQuery request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);
        return ApiResult(result);
    }

    [HttpGet]
    [SwaggerOperation("Get Total Count")]
    [SwaggerResponse(StatusCodes.Status200OK, "Retrieved", typeof(int))]
    public async Task<IActionResult> GetTotalCount(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetSampleModelTotalCountQuery(), cancellationToken);
        return ApiResult(result);
    }

    [HttpPost]
    [Authorize]
    [Idempotent]
    [SwaggerOperation("Create")]
    [SwaggerResponse(StatusCodes.Status200OK, "Created", typeof(void))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Validation Error Occured", typeof(void))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized", typeof(void))]
    public async Task<IActionResult> Create(CreateSampleModelCommand request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);
        return ApiResult(result);
    }

    [HttpPut]
    [Authorize]
    [SwaggerOperation("Update")]
    [SwaggerResponse(StatusCodes.Status200OK, "Updated", typeof(void))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Validation Error Occured", typeof(void))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized", typeof(void))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Not Found", typeof(void))]
    public async Task<IActionResult> Update(UpdateSampleModelCommand request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);
        return ApiResult(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "CanDeletePolicy")]
    [SwaggerOperation("Delete")]
    [SwaggerResponse(StatusCodes.Status200OK, "Deleted", typeof(void))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Validation Error Occured", typeof(void))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized", typeof(void))]
    [SwaggerResponse(StatusCodes.Status403Forbidden, "Access Denied", typeof(void))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Not Found", typeof(void))]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new DeleteSampleModelCommand(id), cancellationToken);
        return ApiResult(result);
    }

    [HttpGet]
    [SwaggerOperation("Get Gender Enum")]
    [SwaggerResponse(StatusCodes.Status200OK, "Retrieved", typeof(List<EnumViewModel>))]
    public async Task<IActionResult> GenderEnum(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetGenderEnumQuery(), cancellationToken);
        return ApiResult(result);
    }
}
