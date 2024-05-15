using FluentValidation.Results;

namespace BuildingBlocks.Application.Exceptions;

public class BadRequestException(List<ValidationFailure> failures) : Exception
{
    public List<ValidationFailure> Errors { get; } = failures;
}
