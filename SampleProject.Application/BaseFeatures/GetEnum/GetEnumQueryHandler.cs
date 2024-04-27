using Humanizer;
using SampleProject.Application.BaseViewModels;

namespace SampleProject.Application.BaseFeatures.GetEnum;

public class GetEnumQueryHandler<TEnum> : IBaseCommandQueryHandler<GetEnumQuery<TEnum>, IList<EnumViewModel>>
    where TEnum : Enum
{
    public async Task<BaseResult<IList<EnumViewModel>>> Handle(GetEnumQuery<TEnum> request, CancellationToken cancellationToken)
    {
        var result = new BaseResult<IList<EnumViewModel>>();
        var data = new List<EnumViewModel>();

        await Task.Run(() =>
        {
            var enumList = Enum.GetValues(typeof(TEnum)).Cast<TEnum>();
            data = enumList.Select(x => new EnumViewModel
            (
                Convert.ToInt32(x),
                x.ToString(),
                x.Humanize()
            )).ToList();
        }, cancellationToken);

        result.AddValue(data);
        result.Success();
        return result;
    }
}