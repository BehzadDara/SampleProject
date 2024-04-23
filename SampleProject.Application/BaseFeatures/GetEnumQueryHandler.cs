using Humanizer;
using SampleProject.Application.BaseFeature;
using SampleProject.Application.BaseViewModels;

namespace SampleProject.Application.BaseFeatures;

public abstract class GetEnumQueryHandler<TEnum> : IBaseCommandQueryHandler<GetEnumQuery, IList<EnumViewModel>>
    where TEnum : Enum
{
    public async Task<BaseResult<IList<EnumViewModel>>> Handle(GetEnumQuery request, CancellationToken cancellationToken)
    {
        var result = new BaseResult<IList<EnumViewModel>>();
        var data = new List<EnumViewModel>();

        await Task.Run(() =>
        {
            var enumList = Enum.GetValues(typeof(TEnum)).Cast<TEnum>();
            data = enumList.Select(x => new EnumViewModel
            (
                //(int)x,
                0,
                x.ToString(),
                x.Humanize()
            )).ToList();
        }, cancellationToken);

        result.AddValue(data);
        result.Success();
        return result;
    }
}