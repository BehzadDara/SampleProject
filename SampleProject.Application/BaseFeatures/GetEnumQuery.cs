using SampleProject.Application.BaseFeature;
using SampleProject.Application.BaseViewModels;

namespace SampleProject.Application.BaseFeatures;

public abstract record GetEnumQuery(
    ) : IBaseCommandQuery<IList<EnumViewModel>>;
