using SampleProject.Application.BaseFeatures;
using SampleProject.Application.BaseViewModels;

namespace SampleProject.Application.Features.SampleModel.Queries.GetGenderEnum;

public record GetGenderEnumQuery(
    ) : IBaseCommandQuery<IList<EnumViewModel>>;
