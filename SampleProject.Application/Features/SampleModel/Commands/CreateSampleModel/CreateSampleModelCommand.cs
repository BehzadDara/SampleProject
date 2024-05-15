using BuildingBlocks.Application.Features;
using SampleProject.Domain.Enums;

namespace SampleProject.Application.Features.SampleModel.Commands.CreateSampleModel;

public record CreateSampleModelCommand(
    string FirstName,
    string LastName,
    int Age,
    GenderEnum Gender,
    string Address
    ) : ICommandQuery;