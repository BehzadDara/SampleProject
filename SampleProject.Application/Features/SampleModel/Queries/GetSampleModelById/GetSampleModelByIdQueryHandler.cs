using BuildingBlocks.Application.Exceptions;
using BuildingBlocks.Application.Features;
using SampleProject.Application.ViewModels;
using SampleProject.Domain.Interfaces;

namespace SampleProject.Application.Features.SampleModel.Queries.GetSampleModelById;

public class GetSampleModelByIdQueryHandler(ISampleProjectUnitOfWork unitOfWork) : ICommandQueryHandler<GetSampleModelByIdQuery, SampleModelViewModel>
{
    public async Task<Result<SampleModelViewModel>> Handle(GetSampleModelByIdQuery request, CancellationToken cancellationToken)
    {
        var existEntity = await unitOfWork.SampleModelRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException(BuildingBlocks.Resources.Messages.NotFound);

        var viewModel = existEntity.ToViewModel();

        var result = new Result<SampleModelViewModel>();
        result.AddValue(viewModel);
        result.OK();
        return result;
    }
}