using MediatR;
using SampleProject.Application.BaseFeatures;
using SampleProject.Application.BaseFeatures.GetEnum;
using SampleProject.Application.BaseViewModels;
using SampleProject.Domain.Enums;

namespace SampleProject.Application.Features.SampleModel.Queries.GetGenderEnum;

public class GetGenderEnumQueryHandler(IMediator mediator) : IBaseCommandQueryHandler<GetGenderEnumQuery, IList<EnumViewModel>>
{
    public async Task<BaseResult<IList<EnumViewModel>>> Handle(GetGenderEnumQuery request, CancellationToken cancellationToken)
    {
        return await mediator.Send(new GetEnumQuery<GenderEnum>(), cancellationToken);
    }
}
