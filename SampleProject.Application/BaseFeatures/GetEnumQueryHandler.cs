using Humanizer;
using SampleProject.Application.BaseViewModels;

namespace SampleProject.Application.BaseFeatures;

public class GetEnumQueryHandler<TRequest, TEnum> where TEnum : Enum
{
    public async virtual Task<BaseResult<IList<EnumViewModel>>> Handle(TRequest request, CancellationToken cancellationToken)
    {
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

        var result = new BaseResult<IList<EnumViewModel>>();
        result.AddValue(data);
        result.OK();
        return result;
    }
}