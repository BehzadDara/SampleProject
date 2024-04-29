using SampleProject.Application.BaseExceptions;
using SampleProject.Application.BaseFeatures;
using SampleProject.Application.ViewModels;
using SampleProject.Domain.Interfaces;

namespace SampleProject.Application.Features.SampleModel.Queries.GetSampleModelById;

public class GetSampleModelByIdQueryHandler(IUnitOfWork unitOfWork) : IBaseCommandQueryHandler<GetSampleModelByIdQuery, SampleModelViewModel>
{
    public async Task<BaseResult<SampleModelViewModel>> Handle(GetSampleModelByIdQuery request, CancellationToken cancellationToken)
    {
        var existEntity = await unitOfWork.SampleModelRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new BaseNotFoundException();

        var viewModel = existEntity.ToViewModel();

        var result = new BaseResult<SampleModelViewModel>();
        result.AddValue(viewModel);
        result.Success();
        return result;
    }
}