using FluentValidation.Results;

namespace SampleProject.Application.BaseExceptions;

public class ValidationException : ApplicationException
{
    public ValidationException()
        : base(Resources.Messages.BadRequest)
    {

    }

    public ValidationException(IEnumerable<ValidationFailure> failures) : this()
    {
        Errors = failures.GroupBy(x => x.PropertyName, x => x.ErrorMessage)
            .ToDictionary(x => x.Key, x => x.ToList());
    }

    public IDictionary<string, List<string>> Errors { get; set; } = new Dictionary<string, List<string>>();
}
