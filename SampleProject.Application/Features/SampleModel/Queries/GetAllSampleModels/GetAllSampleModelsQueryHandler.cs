using BuildingBlocks.Application.Features;
using SampleProject.Application.ViewModels;
using SampleProject.Domain.Interfaces;

namespace SampleProject.Application.Features.SampleModel.Queries.GetAllSampleModels;

public class GetAllSampleModelsQueryHandler(ISampleProjectUnitOfWork unitOfWork) : ICommandQueryHandler<GetAllSampleModelsQuery, IReadOnlyList<SampleModelViewModel>>
{
    public async Task<Result<IReadOnlyList<SampleModelViewModel>>> Handle(GetAllSampleModelsQuery request, CancellationToken cancellationToken)
    {
        var entities = await unitOfWork.SampleModelRepository.GetAllAsync(cancellationToken);
        var viewModels = entities.ToViewModel();

        var result = new Result<IReadOnlyList<SampleModelViewModel>>();
        result.AddValue(viewModels);
        result.OK();
        return result;
    }
}