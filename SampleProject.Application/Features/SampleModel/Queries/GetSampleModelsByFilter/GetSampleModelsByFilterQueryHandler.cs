using SampleProject.Application.BaseFeatures;
using SampleProject.Application.Specifications.SampleModel;
using SampleProject.Application.ViewModels;
using SampleProject.Domain.Interfaces;

namespace SampleProject.Application.Features.SampleModel.Queries.GetSampleModelsByFilter;

public class GetSampleModelsByFilterQueryHandler(IUnitOfWork unitOfWork) : IBaseCommandQueryHandler<GetSampleModelsByFilterQuery, PagedList<SampleModelViewModel>>
{
    public async Task<BaseResult<PagedList<SampleModelViewModel>>> Handle(GetSampleModelsByFilterQuery request, CancellationToken cancellationToken)
    {
        var result = new BaseResult<PagedList<SampleModelViewModel>>();

        var specification = new GetSampleModelsByFilterSpecification(request);
        var (TotalCount, Data) = await unitOfWork.SampleModelRepository.GetByFilter(specification, cancellationToken);

        var viewModel = Data.ToViewModel();
        var pagedList = PagedList<SampleModelViewModel>.Create(request.PageSize, request.PageNumber, TotalCount, viewModel);

        result.AddValue(pagedList);
        result.Success();
        return result;
    }
}