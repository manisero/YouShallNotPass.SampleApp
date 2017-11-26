using System.Collections.Generic;
using Manisero.YouShallNotPass.SampleApp.Validation.Validations;
using Manisero.YouShallNotPass.Validations;

namespace Manisero.YouShallNotPass.SampleApp.Validation
{
    public class ValidationRules
    {
        public static readonly IValidationRule<int> UserIdValidationRule = new UserExistsValidationRule();

        public static readonly IValidationRule<string> UserEmailValidationRule = new AllValidationRule<string>
        {
            Rules = new List<IValidationRule<string>>
            {
                new NotNullValidationRule<string>(),
                new EmailValidationRule()
            }
        };

        public static readonly IValidationRule<string> UserFirstNameValidationRule = new NotNullNorWhiteSpaceValidationRule();

        public static readonly IValidationRule<string> UserLastNameValidationRule = new NotNullNorWhiteSpaceValidationRule();
    }
}
