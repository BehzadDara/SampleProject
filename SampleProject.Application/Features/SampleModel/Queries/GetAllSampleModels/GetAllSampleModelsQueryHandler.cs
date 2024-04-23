using SampleProject.Application.BaseFeature;
using SampleProject.Application.Mapper;
using SampleProject.Application.ViewModels;
using SampleProject.Domain.Interfaces;

namespace SampleProject.Application.Features.SampleModel.Queries.GetAllSampleModels;

public class GetAllSampleModelsQueryHandler(IUnitOfWork unitOfWork, SampleModelMapper mapper) : IBaseCommandQueryHandler<GetAllSampleModelsQuery, IList<SampleModelViewModel>>
{
    public async Task<BaseResult<IList<SampleModelViewModel>>> Handle(GetAllSampleModelsQuery request, CancellationToken cancellationToken)
    {
        var result = new BaseResult<IList<SampleModelViewModel>>();

        var entities = await unitOfWork.SampleModelRepository.GetAllAsync(cancellationToken);
        var viewModels = mapper.ToViewModel(entities);

        result.AddValue(viewModels);
        result.Success();
        return result;
    }
}