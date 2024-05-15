using BuildingBlocks.Application.Features;
using BuildingBlocks.Application.ViewModels;
using SampleProject.Domain.Enums;

namespace SampleProject.Application.Features.SampleModel.Queries.GetGenderEnum;

public class GetGenderEnumQueryHandler() : 
    GetEnumQueryHandler<GetGenderEnumQuery, GenderEnum>,
    ICommandQueryHandler<GetGenderEnumQuery, IList<EnumViewModel>>
{
}
