using FluentValidation.Results;

namespace SampleProject.Application.BaseExceptions;

public class BaseValidationException(List<ValidationFailure> failures) : Exception
{
    public List<ValidationFailure> Errors { get; } = failures;
}
