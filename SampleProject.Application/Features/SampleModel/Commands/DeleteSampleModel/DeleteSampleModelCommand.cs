using BuildingBlocks.Application.Features;

namespace SampleProject.Application.Features.SampleModel.Commands.DeleteSampleModel;

public record DeleteSampleModelCommand(
    Guid Id
    ) : ICommandQuery;