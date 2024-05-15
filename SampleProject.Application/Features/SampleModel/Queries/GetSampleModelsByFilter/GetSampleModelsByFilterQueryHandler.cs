using BuildingBlocks.Application.Features;
using SampleProject.Application.Specifications.SampleModel;
using SampleProject.Application.ViewModels;
using SampleProject.Domain.Interfaces;

namespace SampleProject.Application.Features.SampleModel.Queries.GetSampleModelsByFilter;

public class GetSampleModelsByFilterQueryHandler(ISampleProjectUnitOfWork unitOfWork) : ICommandQueryHandler<GetSampleModelsByFilterQuery, PagedList<SampleModelViewModel>>
{
    public async Task<Result<PagedList<SampleModelViewModel>>> Handle(GetSampleModelsByFilterQuery request, CancellationToken cancellationToken)
    {
        var specification = new GetSampleModelsByFilterSpecification(request);
        var (totalCount, data) = await unitOfWork.SampleModelRepository.ListAsync(specification, cancellationToken);

        var viewModel = data.ToViewModel();
        var pagedList = PagedList<SampleModelViewModel>.Create(request.PageSize, request.PageNumber, totalCount, viewModel);

        var result = new Result<PagedList<SampleModelViewModel>>();
        result.AddValue(pagedList);
        result.OK();
        return result;
    }
}