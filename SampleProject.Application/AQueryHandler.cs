using MediatR;
using SampleProject.Application.BaseFeature;

namespace SampleProject.Application;

public class AQueryHandler : IRequestHandler<AQuery, BaseResult<string>>
{
    public Task<BaseResult<string>> Handle(AQuery request, CancellationToken cancellationToken)
    {
        var y = new BaseResult<string>();
        y.AddValue($"{request.X} {request.X}");
        return Task.FromResult(y);
    }
}