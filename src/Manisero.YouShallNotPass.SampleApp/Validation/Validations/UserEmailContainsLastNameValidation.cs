using System;

namespace Manisero.YouShallNotPass.SampleApp.Validation.Validations
{
    public static class UserEmailContainsLastNameValidation
    {
        public const string Code = "UserEmailContainsLastName";

        public struct Input
        {
            public string Email { get; set; }
            public string LastName { get; set; }

            public Input(string email, string lastName)
            {
                Email = email;
                LastName = lastName;
            }
        }

        public class Rule : IValidationRule<Input, Error>
        {
        }

        public class Error
        {
            public static readonly Error Instance = new Error();
        }

        public static readonly Func<Input, bool> Validator
            = v => v.Email.Contains(v.LastName);
    }
}
