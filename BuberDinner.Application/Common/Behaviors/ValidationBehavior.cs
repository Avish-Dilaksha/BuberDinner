using MediatR;
using ErrorOr;
using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Authentication.Common;
using FluentValidation;
using FluentValidation.Results;

namespace BuberDinner.Application.Common.Behaviors;

public class ValidateRegisterCommandBehavior :
    IPipelineBehavior<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IValidator<RegisterCommand> _validator;

    public ValidateRegisterCommandBehavior(IValidator<RegisterCommand> validator)
    {
        _validator = validator;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(
        RegisterCommand request, 
        RequestHandlerDelegate<ErrorOr<AuthenticationResult>> next, 
        CancellationToken cancellationToken)
    {
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

        return errors;
    }
}