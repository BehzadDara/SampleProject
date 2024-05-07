using SampleProject.Application.BaseFeatures;
using SampleProject.Domain.Interfaces;

namespace SampleProject.Application.Features.AnotherSampleModel.Queries.GetAnotherSampleModelTotalCount;

public class GetAnotherSampleModelTotalCountQueryHandler(IUnitOfWork unitOfWork) : IBaseCommandQueryHandler<GetAnotherSampleModelTotalCountQuery, int>
{
    public async Task<BaseResult<int>> Handle(GetAnotherSampleModelTotalCountQuery request, CancellationToken cancellationToken)
    {
        var totalCount = await unitOfWork.AnotherSampleModelRepository.GetTotalCount(cancellationToken);

        var result = new BaseResult<int>();
        result.AddValue(totalCount);
        result.OK();
        return result;
    }
}