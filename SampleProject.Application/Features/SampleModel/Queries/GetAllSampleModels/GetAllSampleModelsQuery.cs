using SampleProject.Application.BaseFeature;
using SampleProject.Application.ViewModels;

namespace SampleProject.Application.Features.SampleModel.Queries.GetAllSampleModels;

public record GetAllSampleModelsQuery(
    ) : IBaseCommandQuery<IList<SampleModelViewModel>>;
