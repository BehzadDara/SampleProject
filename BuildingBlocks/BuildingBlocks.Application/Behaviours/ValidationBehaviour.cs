using FluentValidation;
using MediatR;
using BuildingBlocks.Application.Exceptions;
using FluentValidation.Results;
using Humanizer;

namespace BuildingBlocks.Application.Behaviours;

public class ValidationBehaviour<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) 
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken = default)
    {
        if (validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            var validationResult = await validators.First().ValidateAsync(context, cancellationToken);

            if (!validationResult.IsValid)
            {
                var failures = Serialize(validationResult.Errors);
                throw new BadRequestException(Resources.Messages.BadRequest, failures);
            }
        }
        return await next();
    }

    private static Dictionary<string, string[]> Serialize(IEnumerable<ValidationFailure> failures)
    {
        var camelCaseFailures = failures
            .GroupBy(failure => failure.PropertyName.Camelize())
            .ToDictionary(
                group => group.Key,
                group => group.Select(failure => failure.ErrorMessage).ToArray()
            );

        return camelCaseFailures;
    }
}