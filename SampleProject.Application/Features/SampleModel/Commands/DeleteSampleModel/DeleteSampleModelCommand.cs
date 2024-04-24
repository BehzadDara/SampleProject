using SampleProject.Application.BaseFeatures;

namespace SampleProject.Application.Features.SampleModel.Commands.DeleteSampleModel;

public record DeleteSampleModelCommand(
    Guid Id
    ) : IBaseCommandQuery;