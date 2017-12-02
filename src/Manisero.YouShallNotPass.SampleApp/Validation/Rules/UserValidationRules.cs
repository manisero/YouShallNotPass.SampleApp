using System.Collections.Generic;
using Manisero.YouShallNotPass.SampleApp.Validation.Validations;
using Manisero.YouShallNotPass.Validations;

namespace Manisero.YouShallNotPass.SampleApp.Validation.Rules
{
    public static class UserValidationRules
    {
        public static readonly IValidationRule<int> UserIdRule = new UserExistsValidation.Rule();

        public static readonly IValidationRule<string> EmailRule = new AllValidation.Rule<string>
        {
            Rules = new List<IValidationRule<string>>
            {
                new NotNullValidation.Rule<string>(),
                new EmailValidation.Rule()
            }
        };

        public static readonly IValidationRule<string> FirstNameRule = new NotNullNorWhiteSpaceValidation.Rule();

        public static readonly IValidationRule<string> LastNameRule = new NotNullNorWhiteSpaceValidation.Rule();
    }
}
