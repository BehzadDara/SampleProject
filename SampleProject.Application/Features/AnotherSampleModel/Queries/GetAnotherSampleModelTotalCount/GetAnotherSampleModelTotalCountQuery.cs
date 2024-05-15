using BuildingBlocks.Application.Features;

namespace SampleProject.Application.Features.AnotherSampleModel.Queries.GetAnotherSampleModelTotalCount;

public record GetAnotherSampleModelTotalCountQuery(
) : ICommandQuery<int>;