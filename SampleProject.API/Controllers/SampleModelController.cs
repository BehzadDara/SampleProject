using MediatR;
using Microsoft.AspNetCore.Mvc;
using SampleProject.Application.Features.SampleModel.Commands.CreateSampleModel;
using SampleProject.Application.Features.SampleModel.Commands.DeleteSampleModel;
using SampleProject.Application.Features.SampleModel.Commands.UpdateSampleModel;

namespace SampleProject.API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class SampleModelController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public void Create(CreateSampleModelCommand request)
    {
        mediator.Send(request);
    }

    [HttpPut]
    public void Update(UpdateSampleModelCommand request)
    {
        mediator.Send(request);
    }

    [HttpDelete]
    public void Delete(Guid id)
    {
        mediator.Send(new DeleteSampleModelCommand(id));
    }
}
