using BuildingBlocks.Application.Features;

namespace SampleProject.Application.Features.SampleModel.Queries.GetSampleModelTotalCount;

public record GetSampleModelTotalCountQuery(
    ) : ICommandQuery<int>;