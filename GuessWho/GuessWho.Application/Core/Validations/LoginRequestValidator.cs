using FluentValidation;
using GuessWho.Application.Core.Errors;
using GuessWho.Domain.Requests;

namespace GuessWho.Application.Core.Validations;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithErrorCode(ValidationErrors.Login.EmailIsMandatory.Code)
            .WithMessage(ValidationErrors.Login.EmailIsMandatory.ErrorMessage)
            .EmailAddress()
            .WithErrorCode(ValidationErrors.Login.EmailIsInvalid.Code)
            .WithMessage(ValidationErrors.Login.EmailIsInvalid.ErrorMessage);
        
        RuleFor(x => x.Password).NotEmpty().WithErrorCode(ValidationErrors.Login.PasswordIsMandatory.Code)
            .WithMessage(ValidationErrors.Login.PasswordIsMandatory.ErrorMessage);
    }
}