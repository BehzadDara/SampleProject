using SampleProject.Application.BaseFeatures;
using SampleProject.Domain.Enums;

namespace SampleProject.Application.Features.SampleModel.Commands.UpdateSampleModel;

public record UpdateSampleModelCommand(
    Guid Id,
    string FirstName,
    string LastName,
    int Age,
    GenderEnum Gender,
    string Address
    ) : IBaseCommandQuery;