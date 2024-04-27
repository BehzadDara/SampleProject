using SampleProject.Application.BaseViewModels;

namespace SampleProject.Application.BaseFeatures.GetEnum;

public record GetEnumQuery<TEnum>(
    ) : IBaseCommandQuery<IList<EnumViewModel>>
     where TEnum : Enum;
