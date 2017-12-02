using Manisero.YouShallNotPass.SampleApp.Commands;
using Manisero.YouShallNotPass.SampleApp.Repositories;
using Manisero.YouShallNotPass.SampleApp.Validation.Validations;
using Manisero.YouShallNotPass.SampleApp.Validation.Validations.Generic;

namespace Manisero.YouShallNotPass.SampleApp.Validation.Factories
{
    public class ValidationEngineFactory
    {
        public IValidationEngine Create(IUserRepository userRepository)
        {
            var builder = new ValidationEngineBuilder();

            RegisterValidators(builder, userRepository);
            RegisterRules(builder);

            return builder.Build();
        }

        private void RegisterValidators(
            IValidationEngineBuilder builder,
            IUserRepository userRepository)
        {
            // Generic
            builder.RegisterFullGenericValidator(typeof(BetweenValidation.Validator<>));
            builder.RegisterFullGenericValidator(typeof(IsEnumValueValidation.Validator<>));
            builder.RegisterFullGenericValidator(typeof(NullValidation.Validator<>));

            // Specific
            builder.RegisterFullValidator(new Algorithm4ConfigurationPhasesKeysValidation.Validator());

            builder.RegisterValueOnlyBoolValidatorFunc<UserEmailContainsLastNameValidation.Rule,
                                                       UserEmailContainsLastNameValidation.Input,
                                                       UserEmailContainsLastNameValidation.Error>(
                                                       UserEmailContainsLastNameValidation.Validator,
                                                       UserEmailContainsLastNameValidation.Error.Instance);

            builder.RegisterFullValidator(new UserEmailUniqueValidation.Validator(userRepository));
            builder.RegisterFullValidator(new UserExistsValidation.Validator(userRepository));
        }

        private void RegisterRules(IValidationEngineBuilder builder)
        {
            builder.RegisterValidationRule(typeof(CreateUserCommand), CreateUserCommand.ValidationRule);
            builder.RegisterValidationRule(typeof(UpdateUserCommand), UpdateUserCommand.ValidationRule);
            builder.RegisterValidationRule(typeof(RunTaskCommand), RunTaskCommand.ValidationRule);
        }
    }
}
