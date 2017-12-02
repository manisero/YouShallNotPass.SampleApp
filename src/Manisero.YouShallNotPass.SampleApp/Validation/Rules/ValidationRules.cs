using System.Collections.Generic;
using Manisero.YouShallNotPass.SampleApp.Validation.Validations;
using Manisero.YouShallNotPass.Validations;

namespace Manisero.YouShallNotPass.SampleApp.Validation.Rules
{
    public class ValidationRules
    {
        public static readonly IValidationRule<int> UserIdValidationRule = new UserExistsValidationRule();

        public static readonly IValidationRule<string> UserEmailValidationRule = new AllValidation.Rule<string>
        {
            Rules = new List<IValidationRule<string>>
            {
                new NotNullValidation.Rule<string>(),
                new EmailValidation.Rule()
            }
        };

        public static readonly IValidationRule<string> UserFirstNameValidationRule = new NotNullNorWhiteSpaceValidation.Rule();

        public static readonly IValidationRule<string> UserLastNameValidationRule = new NotNullNorWhiteSpaceValidation.Rule();
    }
}
