using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleProject.API.BaseControllers;
using SampleProject.Application.BaseViewModels;
using SampleProject.Application.Features.SampleModel.Commands.CreateSampleModel;
using SampleProject.Application.Features.SampleModel.Commands.DeleteSampleModel;
using SampleProject.Application.Features.SampleModel.Commands.UpdateSampleModel;
using SampleProject.Application.Features.SampleModel.Queries.GetAllSampleModels;
using SampleProject.Application.Features.SampleModel.Queries.GetGenderEnum;
using SampleProject.Application.Features.SampleModel.Queries.GetSampleModelById;
using SampleProject.Application.Features.SampleModel.Queries.GetSampleModelsByFilter;
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
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetSampleModelByIdQuery(id), cancellationToken);
        return BaseApiResult(result);
    }

    [HttpGet]
    [SwaggerOperation("Get All")]
    [SwaggerResponse(StatusCodes.Status200OK, "Retrieved", typeof(List<SampleModelViewModel>))]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAllSampleModelsQuery(), cancellationToken);
        return BaseApiResult(result);
    }

    [HttpGet]
    [SwaggerOperation("Get By Filter")]
    [SwaggerResponse(StatusCodes.Status200OK, "Retrieved", typeof(List<SampleModelViewModel>))]
    public async Task<IActionResult> GetByFilter([FromQuery] GetSampleModelsByFilterQuery request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);
        return BaseApiResult(result);
    }

    [HttpPost]
    [Authorize]
    [SwaggerOperation("Create")]
    [SwaggerResponse(StatusCodes.Status200OK, "Created", typeof(void))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Validation Error Occured", typeof(void))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized", typeof(void))]
    public async Task<IActionResult> Create(CreateSampleModelCommand request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);
        return BaseApiResult(result);
    }

    [HttpPut]
    [Authorize]
    [SwaggerOperation("Update")]
    [SwaggerResponse(StatusCodes.Status200OK, "Created", typeof(void))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Validation Error Occured", typeof(void))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized", typeof(void))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Not Found", typeof(void))]
    public async Task<IActionResult> Update(UpdateSampleModelCommand request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);
        return BaseApiResult(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "CanDeletePolicy")]
    [SwaggerOperation("Update")]
    [SwaggerResponse(StatusCodes.Status200OK, "Created", typeof(void))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Validation Error Occured", typeof(void))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized", typeof(void))]
    [SwaggerResponse(StatusCodes.Status403Forbidden, "Access Denied", typeof(void))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Not Found", typeof(void))]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new DeleteSampleModelCommand(id), cancellationToken);
        return BaseApiResult(result);
    }

    [HttpGet]
    [SwaggerOperation("Get Gender Enum")]
    [SwaggerResponse(StatusCodes.Status200OK, "Retrieved", typeof(List<EnumViewModel>))]
    public async Task<IActionResult> GenderEnum(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetGenderEnumQuery(), cancellationToken);
        return BaseApiResult(result);
    }
}
