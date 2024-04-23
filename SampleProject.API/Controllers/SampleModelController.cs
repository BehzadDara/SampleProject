using MediatR;
using Microsoft.AspNetCore.Mvc;
using SampleProject.Application;
using SampleProject.Application.Features.SampleModel.Commands.CreateSampleModel;
using SampleProject.Application.Features.SampleModel.Commands.DeleteSampleModel;
using SampleProject.Application.Features.SampleModel.Commands.UpdateSampleModel;
using SampleProject.Application.Features.SampleModel.Queries.GetAllSampleModels;
using SampleProject.Application.Features.SampleModel.Queries.GetGenderEnum;
using SampleProject.Application.Features.SampleModel.Queries.GetSampleModelById;
using System.Runtime.CompilerServices;

namespace SampleProject.API.Controllers;

public class SampleModelController(IMediator mediator) : BaseController
{
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await mediator.Send(new GetSampleModelByIdQuery(id));
        return BaseApiResult(result);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var result = await mediator.Send(new GetAllSampleModelsQuery());
        return BaseApiResult(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Create(CreateSampleModelCommand request)
    {
        var result = await mediator.Send(request);
        return BaseApiResult(result);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(UpdateSampleModelCommand request)
    {
        var result = await mediator.Send(request);
        return BaseApiResult(result);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await mediator.Send(new DeleteSampleModelCommand(id));
        return BaseApiResult(result);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GenderEnum()
    {
        var result = await mediator.Send(new GetGenderEnumQuery());
        return BaseApiResult(result);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetA()
    {
        var result = await mediator.Send(new AQuery("Hi"));
        return BaseApiResult(result);
    }
}
