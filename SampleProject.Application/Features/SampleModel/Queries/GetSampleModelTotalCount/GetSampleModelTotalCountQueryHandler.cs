using BuildingBlocks.Application.Features;
using SampleProject.Domain.Interfaces;

namespace SampleProject.Application.Features.SampleModel.Queries.GetSampleModelTotalCount;

public class GetSampleModelTotalCountQueryHandler(ISampleProjectUnitOfWork unitOfWork) : ICommandQueryHandler<GetSampleModelTotalCountQuery, int>
{
    public async Task<Result<int>> Handle(GetSampleModelTotalCountQuery request, CancellationToken cancellationToken)
    {
        var totalCount = await unitOfWork.SampleModelRepository.GetTotalCount(cancellationToken);

        var result = new Result<int>();
        result.AddValue(totalCount);
        result.OK();
        return result;
    }
}