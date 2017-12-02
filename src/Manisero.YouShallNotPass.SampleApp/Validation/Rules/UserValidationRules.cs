using System.Collections.Generic;
using Manisero.YouShallNotPass.SampleApp.Validation.Validations;
using Manisero.YouShallNotPass.Validations;

namespace Manisero.YouShallNotPass.SampleApp.Validation.Rules
{
    public static class UserValidationRules
    {
        public static readonly IValidationRule<int> UserIdValidationRule = new UserExistsValidationRule();

        public static readonly IValidationRule<string> EmailValidationRule = new AllValidation.Rule<string>
        {
            Rules = new List<IValidationRule<string>>
            {
                new NotNullValidation.Rule<string>(),
                new EmailValidation.Rule()
            }
        };

        public static readonly IValidationRule<string> FirstNameValidationRule = new NotNullNorWhiteSpaceValidation.Rule();

        public static readonly IValidationRule<string> LastNameValidationRule = new NotNullNorWhiteSpaceValidation.Rule();
    }
}
