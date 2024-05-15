using BuildingBlocks.Application.Features;
using SampleProject.Domain.Interfaces;

namespace SampleProject.Application.Features.AnotherSampleModel.Queries.GetAnotherSampleModelTotalCount;

public class GetAnotherSampleModelTotalCountQueryHandler(ISampleProjectUnitOfWork unitOfWork) : ICommandQueryHandler<GetAnotherSampleModelTotalCountQuery, int>
{
    public async Task<Result<int>> Handle(GetAnotherSampleModelTotalCountQuery request, CancellationToken cancellationToken)
    {
        var totalCount = await unitOfWork.AnotherSampleModelRepository.GetTotalCount(cancellationToken);

        var result = new Result<int>();
        result.AddValue(totalCount);
        result.OK();
        return result;
    }
}