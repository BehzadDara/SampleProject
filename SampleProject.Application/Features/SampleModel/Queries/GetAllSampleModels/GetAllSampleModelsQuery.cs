using BuildingBlocks.Application.Features;
using SampleProject.Application.ViewModels;

namespace SampleProject.Application.Features.SampleModel.Queries.GetAllSampleModels;

public record GetAllSampleModelsQuery(
    ) : ICommandQuery<IReadOnlyList<SampleModelViewModel>>;
