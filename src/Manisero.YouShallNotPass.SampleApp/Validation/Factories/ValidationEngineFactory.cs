﻿using Manisero.YouShallNotPass.SampleApp.Commands;
using Manisero.YouShallNotPass.SampleApp.Repositories;
using Manisero.YouShallNotPass.SampleApp.Validation.Validations;

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
            builder.RegisterFullGenericValidator(typeof(PropertyValidator<,>));

            // Specific
            builder.RegisterValueOnlyBoolValidatorFunc<UserEmailContainsLastNameValidationRule,
                                                       UserEmailContainsLastNameValidationInput, 
                                                       UserEmailContainsLastNameValidationError>(
                                                           UserEmailContainsLastNameValidator.Func, 
                                                           UserEmailContainsLastNameValidationError.Instance);

            builder.RegisterFullValidator(new UserEmailUniqueValidator(userRepository));
            builder.RegisterFullValidator(new UserExistsValidator(userRepository));
        }

        private void RegisterRules(IValidationEngineBuilder builder)
        {
            builder.RegisterValidationRule(typeof(CreateUserCommand), CreateUserCommand.ValidationRule);
            builder.RegisterValidationRule(typeof(UpdateUserCommand), UpdateUserCommand.ValidationRule);
        }
    }
}