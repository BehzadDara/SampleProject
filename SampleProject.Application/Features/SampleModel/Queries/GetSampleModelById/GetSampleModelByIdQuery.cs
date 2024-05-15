using BuildingBlocks.Application.Features;
using SampleProject.Application.ViewModels;

namespace SampleProject.Application.Features.SampleModel.Queries.GetSampleModelById;

public record GetSampleModelByIdQuery(
    Guid Id
    ) : ICommandQuery<SampleModelViewModel>;
