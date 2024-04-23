using SampleProject.Application.BaseFeature;
using SampleProject.Application.ViewModels;

namespace SampleProject.Application.Features.SampleModel.Queries.GetSampleModelById;

public record GetSampleModelByIdQuery(
    Guid Id
    ) : IBaseCommandQuery<SampleModelViewModel>;
