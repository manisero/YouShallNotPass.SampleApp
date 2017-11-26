using System;

namespace Manisero.YouShallNotPass.SampleApp.Validation.Validations
{
    public struct UserEmailContainsLastNameValidationInput
    {
        public string Email { get; set; }
        public string LastName { get; set; }

        public UserEmailContainsLastNameValidationInput(string email, string lastName)
        {
            Email = email;
            LastName = lastName;
        }
    }

    public class UserEmailContainsLastNameValidationRule : IValidationRule<UserEmailContainsLastNameValidationInput, UserEmailContainsLastNameValidationError>
    {
    }

    public class UserEmailContainsLastNameValidationError
    {
        public const string Code = "UserEmailContainsLastName";
        public static readonly UserEmailContainsLastNameValidationError Instance = new UserEmailContainsLastNameValidationError();
    }

    public static class UserEmailContainsLastNameValidator
    {
        public static readonly Func<UserEmailContainsLastNameValidationInput, bool> Func
            = v => v.Email.Contains(v.LastName);
    }
}
