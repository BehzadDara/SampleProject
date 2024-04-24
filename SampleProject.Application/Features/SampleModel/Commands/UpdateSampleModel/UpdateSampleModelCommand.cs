using SampleProject.Application.BaseFeatures;

namespace SampleProject.Application.Features.SampleModel.Commands.UpdateSampleModel;

public record UpdateSampleModelCommand(
    Guid Id,
    string FirstName,
    string LastName,
    int Age,
    int Gender,
    string Address
    ) : IBaseCommandQuery;