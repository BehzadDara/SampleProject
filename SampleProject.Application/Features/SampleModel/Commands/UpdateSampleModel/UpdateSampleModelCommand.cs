using BuildingBlocks.Application.Features;
using SampleProject.Domain.Enums;

namespace SampleProject.Application.Features.SampleModel.Commands.UpdateSampleModel;

public record UpdateSampleModelCommand(
    int Id,
    string FirstName,
    string LastName,
    int Age,
    GenderEnum Gender,
    string Address
    ) : ICommandQuery;