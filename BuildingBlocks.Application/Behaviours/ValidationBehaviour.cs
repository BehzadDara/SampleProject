using FluentValidation;
using MediatR;
using BuildingBlocks.Application.Exceptions;

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
                throw new BadRequestException(validationResult.Errors);
            }
        }
        return await next();
    }
}