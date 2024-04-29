using SampleProject.Application.BaseFeatures;
using SampleProject.Application.ViewModels;
using SampleProject.Domain.Interfaces;

namespace SampleProject.Application.Features.SampleModel.Queries.GetAllSampleModels;

public class GetAllSampleModelsQueryHandler(IUnitOfWork unitOfWork) : IBaseCommandQueryHandler<GetAllSampleModelsQuery, IList<SampleModelViewModel>>
{
    public async Task<BaseResult<IList<SampleModelViewModel>>> Handle(GetAllSampleModelsQuery request, CancellationToken cancellationToken)
    {
        var entities = await unitOfWork.SampleModelRepository.GetAllAsync(cancellationToken);
        var viewModels = entities.ToViewModel();

        var result = new BaseResult<IList<SampleModelViewModel>>();
        result.AddValue(viewModels);
        result.OK();
        return result;
    }
}