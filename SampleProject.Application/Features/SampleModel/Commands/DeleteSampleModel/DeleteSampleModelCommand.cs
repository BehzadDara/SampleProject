using SampleProject.Application.BaseFeature;

namespace SampleProject.Application.Features.SampleModel.Commands.DeleteSampleModel;

public record DeleteSampleModelCommand(
    Guid Id
    ) : IBaseCommandQuery;