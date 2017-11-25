using System.Collections.Generic;
using Manisero.YouShallNotPass.Validations;

namespace Manisero.YouShallNotPass.SampleApp.Validation
{
    public class ValidationRules
    {
        public static readonly IValidationRule<int> UserIdValidationRule = null; // TODO: User should exist

        public static readonly IValidationRule<string> UserEmailValidationRule = new AllValidationRule<string>
        {
            Rules = new List<IValidationRule<string>>
            {
                new NotNullValidationRule<string>(),
                new EmailValidationRule(),
                // TODO: Should be unique
            }
        };

        public static readonly IValidationRule<string> UserFirstNameValidationRule = new NotNullNorWhiteSpaceValidationRule();

        public static readonly IValidationRule<string> UserLastNameValidationRule = new NotNullNorWhiteSpaceValidationRule();
    }
}
