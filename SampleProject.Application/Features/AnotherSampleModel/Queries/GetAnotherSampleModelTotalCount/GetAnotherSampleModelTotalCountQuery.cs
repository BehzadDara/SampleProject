using SampleProject.Application.BaseFeatures;

namespace SampleProject.Application.Features.AnotherSampleModel.Queries.GetAnotherSampleModelTotalCount;

public record GetAnotherSampleModelTotalCountQuery(
) : IBaseCommandQuery<int>;