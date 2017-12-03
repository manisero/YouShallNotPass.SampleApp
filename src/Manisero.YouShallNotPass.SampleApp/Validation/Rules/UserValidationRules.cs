using Manisero.YouShallNotPass.SampleApp.Validation.Validations;
using Manisero.YouShallNotPass.Validations;

namespace Manisero.YouShallNotPass.SampleApp.Validation.Rules
{
    public static class UserValidationRules
    {
        public static readonly IValidationRule<int> UserIdRule = new UserExistsValidation.Rule();

        public static readonly IValidationRule<string> EmailRule = new ValidationRuleBuilder<string>()
            .All(b => b.NotNull(),
                 b => b.Email());

        public static readonly IValidationRule<string> FirstNameRule = new NotNullNorWhiteSpaceValidation.Rule();

        public static readonly IValidationRule<string> LastNameRule = new NotNullNorWhiteSpaceValidation.Rule();
    }
}
