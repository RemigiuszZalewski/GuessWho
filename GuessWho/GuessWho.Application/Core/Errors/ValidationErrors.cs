using GuessWho.Domain.Primitives;

namespace GuessWho.Application.Core.Errors;

public static class ValidationErrors
{
    public static class Login
    {
        public static Error EmailIsMandatory => 
            new Error("Login.EmailIsMandatory", "Email is mandatory.");
        public static Error PasswordIsMandatory => 
            new Error("Login.PasswordIsMandatory", "Password is mandatory.");
        public static Error EmailIsInvalid => 
            new Error("Login.EmailIsInvalid", "Email or password is invalid.");
    }

    public static class Register
    {
        public static Error EmailIsMandatory => 
            new Error("Register.EmailIsMandatory", "Email is mandatory.");
        public static Error PasswordIsMandatory => 
            new Error("Register.PasswordIsMandatory", "Password is mandatory.");
        public static Error EmailIsInvalid => 
            new Error("Register.EmailIsInvalid", "Provided email is invalid.");
        public static Error PasswordDoesNotContainSpecialCharacter => 
            new Error("Register.PasswordDoesNotContainSpecialCharacter",
                "Provided password does not contain special character.");
        public static Error PasswordDoesNotContainUppercaseLetter => 
            new Error("Register.PasswordDoesNotContainUppercaseLetter",
                "Provided password does not contain uppercase letter.");
        public static Error PasswordDoesNotContainLowercaseLetter => 
            new Error("Register.PasswordDoesNotContainLowercaseLetter",
                "Provided password does not contain lowercase letter.");
        public static Error PasswordIsTooShort => 
            new Error("Register.PasswordIsTooShort",
                "Provided password is too short. Password has to be at least 10 characters long.");
        public static Error PasswordDoesNotContainAnyDigit => 
            new Error("Register.PasswordDoesNotContainAnyDigit",
                "Provided password does not contain any digit.");
        public static Error FirstNameIsMandatory =>
            new Error("Register.FirstNameIsMandatory", "Firstname is mandatory");
        public static Error FirstNameIsTooShort =>
            new Error("Register.FirstNameIsTooShort", "Firstname is too short. " +
                                                      "FirstName has to be at least 3 characters long.");
        public static Error FirstNameIsTooLong =>
            new Error("Register.FirstNameIsTooLong", "Firstname is too long. " +
                                                      "FirstName has to be at most 15 characters long.");
        public static Error LastNameIsMandatory =>
            new Error("Register.LastNameIsMandatory", "Lastname is mandatory");
        public static Error LastNameIsTooShort =>
            new Error("Register.LastNameIsTooShort", "Firstname is too short. " +
                                                      "LastName has to be at least 2 characters long.");
        public static Error LastNameIsTooLong =>
            new Error("Register.LastNameIsTooLong", "Firstname is too long. " +
                                                     "LastName has to be at most 15 characters long.");
    }
}