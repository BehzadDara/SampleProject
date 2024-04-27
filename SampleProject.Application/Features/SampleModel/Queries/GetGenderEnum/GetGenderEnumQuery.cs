using SampleProject.Application.BaseFeatures.GetEnum;
using SampleProject.Domain.Enums;

namespace SampleProject.Application.Features.SampleModel.Queries.GetGenderEnum;

public record GetGenderEnumQuery(
    ) : GetEnumQuery<GenderEnum>;
