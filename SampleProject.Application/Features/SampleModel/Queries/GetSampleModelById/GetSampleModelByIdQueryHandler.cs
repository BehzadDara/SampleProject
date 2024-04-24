using SampleProject.Application.BaseFeatures;
using SampleProject.Application.ViewModels;
using SampleProject.Domain.Interfaces;

namespace SampleProject.Application.Features.SampleModel.Queries.GetSampleModelById;

public class GetSampleModelByIdQueryHandler(IUnitOfWork unitOfWork) : IBaseCommandQueryHandler<GetSampleModelByIdQuery, SampleModelViewModel>
{
    public async Task<BaseResult<SampleModelViewModel>> Handle(GetSampleModelByIdQuery request, CancellationToken cancellationToken)
    {
        var result = new BaseResult<SampleModelViewModel>();

        var entity = await unitOfWork.SampleModelRepository.GetByIdAsync(request.Id, cancellationToken);
        if (entity is null)
        {
            result.NotFound();
            return result;
        }

        var viewModel = entity.ToViewModel();

        result.AddValue(viewModel);
        result.Success();
        return result;
    }
}