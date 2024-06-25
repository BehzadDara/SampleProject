using Humanizer;
using BuildingBlocks.Application.ViewModels;

namespace BuildingBlocks.Application.Features;

public class GetEnumQueryHandler<TRequest, TEnum> where TEnum : Enum
{
    public async virtual Task<Result<IList<EnumViewModel>>> Handle(TRequest request, CancellationToken cancellationToken)
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

        var result = new Result<IList<EnumViewModel>>();
        result.AddValue(data);
        result.OK();
        return result;
    }
}