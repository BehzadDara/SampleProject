using SampleProject.Application.BaseFeatures;

namespace SampleProject.Application.Features.SampleModel.Commands.CreateSampleModel;

public record CreateSampleModelCommand(
    string FirstName,
    string LastName,
    int Age,
    int Gender,
    string Address
    ) : IBaseCommandQuery;