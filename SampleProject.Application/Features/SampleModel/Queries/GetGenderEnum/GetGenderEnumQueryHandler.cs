using SampleProject.Application.BaseFeatures;
using SampleProject.Application.BaseViewModels;
using SampleProject.Domain.Enums;

namespace SampleProject.Application.Features.SampleModel.Queries.GetGenderEnum;

public class GetGenderEnumQueryHandler() : 
    GetEnumQueryHandler<GetGenderEnumQuery, GenderEnum>,
    IBaseCommandQueryHandler<GetGenderEnumQuery, IList<EnumViewModel>>
{
}
