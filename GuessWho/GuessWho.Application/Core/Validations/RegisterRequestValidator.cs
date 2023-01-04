using FluentValidation;
using GuessWho.Application.Core.Errors;
using GuessWho.Application.Core.Extensions;
using GuessWho.Domain.Requests;

namespace GuessWho.Application.Core.Validations;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty()
            .WithErrorCode(ValidationErrors.Register.FirstNameIsMandatory.Code)
            .WithMessage(ValidationErrors.Register.FirstNameIsMandatory.ErrorMessage)
            .MinimumLength(3)
            .WithErrorCode(ValidationErrors.Register.FirstNameIsTooShort.Code)
            .WithMessage(ValidationErrors.Register.FirstNameIsTooShort.ErrorMessage)
            .MaximumLength(15)
            .WithErrorCode(ValidationErrors.Register.FirstNameIsTooLong.Code)
            .WithMessage(ValidationErrors.Register.FirstNameIsTooLong.ErrorMessage);
        
        RuleFor(x => x.LastName).NotEmpty()
            .WithErrorCode(ValidationErrors.Register.LastNameIsMandatory.Code)
            .WithMessage(ValidationErrors.Register.LastNameIsMandatory.ErrorMessage)
            .MinimumLength(3)
            .WithErrorCode(ValidationErrors.Register.LastNameIsTooShort.Code)
            .WithMessage(ValidationErrors.Register.LastNameIsTooShort.ErrorMessage)
            .MaximumLength(15)
            .WithErrorCode(ValidationErrors.Register.LastNameIsTooLong.Code)
            .WithMessage(ValidationErrors.Register.LastNameIsTooLong.ErrorMessage);

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithErrorCode(ValidationErrors.Register.EmailIsMandatory.Code)
            .WithMessage(ValidationErrors.Register.EmailIsMandatory.ErrorMessage)
            .EmailAddress().WithErrorCode(ValidationErrors.Register.EmailIsInvalid.Code)
            .WithMessage(ValidationErrors.Register.EmailIsInvalid.ErrorMessage);
        
        RuleFor(x => x.Password).Password();
    }
}