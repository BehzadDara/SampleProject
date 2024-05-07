using SampleProject.Application.BaseFeatures;
using SampleProject.Domain.Interfaces;

namespace SampleProject.Application.Features.SampleModel.Queries.GetSampleModelTotalCount;

public class GetSampleModelTotalCountQueryHandler(IUnitOfWork unitOfWork) : IBaseCommandQueryHandler<GetSampleModelTotalCountQuery, int>
{
    public async Task<BaseResult<int>> Handle(GetSampleModelTotalCountQuery request, CancellationToken cancellationToken)
    {
        var totalCount = await unitOfWork.SampleModelRepository.GetTotalCount(cancellationToken);

        var result = new BaseResult<int>();
        result.AddValue(totalCount);
        result.OK();
        return result;
    }
}