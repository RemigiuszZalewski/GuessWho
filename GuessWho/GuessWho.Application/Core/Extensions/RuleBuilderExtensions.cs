using FluentValidation;
using GuessWho.Application.Core.Errors;

namespace GuessWho.Application.Core.Extensions;

public static class RuleBuilderExtensions
{
    public static IRuleBuilder<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder, int minimumLength = 10)
    {
        var rules = ruleBuilder
            .NotEmpty()
            .WithErrorCode(ValidationErrors.Register.PasswordIsMandatory.Code)
            .WithMessage(ValidationErrors.Register.PasswordIsMandatory.ErrorMessage)
            .MinimumLength(minimumLength)
            .WithErrorCode(ValidationErrors.Register.PasswordIsTooShort.Code)
            .WithMessage(ValidationErrors.Register.PasswordIsTooShort.ErrorMessage)
            .Matches("[A-Z]")
            .WithErrorCode(ValidationErrors.Register.PasswordDoesNotContainUppercaseLetter.Code)
            .WithMessage(ValidationErrors.Register.PasswordDoesNotContainUppercaseLetter.ErrorMessage)
            .Matches("[a-z]")
            .WithErrorCode(ValidationErrors.Register.PasswordDoesNotContainLowercaseLetter.Code)
            .WithMessage(ValidationErrors.Register.PasswordDoesNotContainLowercaseLetter.ErrorMessage)
            .Matches("[0-9]")
            .WithErrorCode(ValidationErrors.Register.PasswordDoesNotContainAnyDigit.Code)
            .WithMessage(ValidationErrors.Register.PasswordDoesNotContainAnyDigit.ErrorMessage)
            .Matches("[^a-zA-Z0-9]")
            .WithErrorCode(ValidationErrors.Register.PasswordDoesNotContainSpecialCharacter.Code)
            .WithMessage(ValidationErrors.Register.PasswordDoesNotContainSpecialCharacter.ErrorMessage);
        
        return rules;
    }
}