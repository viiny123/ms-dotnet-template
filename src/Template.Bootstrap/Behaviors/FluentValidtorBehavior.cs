using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Template.Application.Base;
using Template.Application.Base.Error;

namespace Template.Bootstrap.Behaviors;

public sealed class FluentValidatorBehavior<TRequest, TResponse> :
    IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
    where TResponse : Result, new()
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public FluentValidatorBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var validationResults = await Task.WhenAll(
            _validators.Select(v => v.ValidateAsync(request, cancellationToken))
        );

        var failures = validationResults
            .SelectMany(r => r.Errors)
            .Where(f => f != null)
            .ToList();

        if (failures.Any())
        {
            var response = new TResponse();
            response.AddValidationErrors(
                failures.Select(f =>
                    new ErrorCatalogEntry(f.ErrorCode, f.ErrorMessage, f.PropertyName)
                ).ToList(),
                StatusCodes.Status400BadRequest
            );
            return response;
        }

        return await next();
    }
}
