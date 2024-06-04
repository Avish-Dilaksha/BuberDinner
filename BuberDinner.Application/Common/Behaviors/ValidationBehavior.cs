using MediatR;
using ErrorOr;
using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Authentication.Common;
using FluentValidation;
using FluentValidation.Results;

namespace BuberDinner.Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse> :
    IPipelineBehavior<TRequest, TResponse>
    where TRequest: IRequest<TResponse>
    where TResponse: IErrorOr
{
    private readonly IValidator<TRequest>? _validator;

    public ValidationBehavior(IValidator<TRequest>? validator = null)
    {
        _validator = validator;
    }

    public async Task<TResponse> Handle(
        TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        if(_validator is null)
        {
            return await next();
        }

        // before the handler
        var vaildationResult = await _validator.ValidateAsync(request, cancellationToken);
        //after the handler
        if(vaildationResult.IsValid)
        {
            return await next();
        }
        var errors = vaildationResult.Errors
                        .ConvertAll(ValidationFailure => Error.Validation(ValidationFailure.PropertyName, ValidationFailure.ErrorMessage))
                        .ToList();

        return (dynamic)errors;
    }
}