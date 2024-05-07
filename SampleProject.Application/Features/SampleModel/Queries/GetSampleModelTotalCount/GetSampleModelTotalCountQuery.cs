using SampleProject.Application.BaseFeatures;

namespace SampleProject.Application.Features.SampleModel.Queries.GetSampleModelTotalCount;

public record GetSampleModelTotalCountQuery(
    ) : IBaseCommandQuery<int>;