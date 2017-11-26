using System.Collections.Generic;
using Manisero.YouShallNotPass.ErrorFormatting;
using Manisero.YouShallNotPass.SampleApp.Utils;
using Manisero.YouShallNotPass.SampleApp.Validation.Validations;
using Manisero.YouShallNotPass.SampleApp.Validation.Validations.BuiltIn;
using Manisero.YouShallNotPass.Validations;

namespace Manisero.YouShallNotPass.SampleApp.Validation.Factories
{
    public class ValidationErrorFormattingEngineFactory
    {
        public IValidationErrorFormattingEngine<IEnumerable<IValidationErrorMessage>> Create()
        {
            return new ValidationErrorFormattingEngineBuilder<IEnumerable<IValidationErrorMessage>>()
                // Built in
                .RegisterFullGenericFormatter(typeof(AllValidationErrorFormatter<>))
                .RegisterFullGenericFormatter(typeof(ComplexValidationErrorFormatter<>))
                .RegisterErrorMessage<EmailValidationError>(BuiltInValidationCodes.Email)
                .RegisterErrorMessage<NotNullValidationError>(BuiltInValidationCodes.NotNull)
                .RegisterErrorMessage<NotNullNorWhiteSpaceValidationError>(BuiltInValidationCodes.NotNullNorWhiteSpace)
                // Custom generic
                .RegisterFullGenericFormatter(typeof(PropertyValidationErrorFormatter<,>))
                // Custom specific
                .RegisterErrorMessage<UserEmailContainsLastNameValidationError>(UserEmailContainsLastNameValidationError.Code)
                .RegisterErrorMessage<UserEmailUniqueValidationError>(UserEmailUniqueValidationError.Code)
                .RegisterErrorMessage<UserExistsValidationError>(UserExistsValidationError.Code)
                .Build();
        }
    }

    public static class ValidationErrorFormattingEngineBuilderExtensions
    {
        public static IValidationErrorFormattingEngineBuilder<IEnumerable<IValidationErrorMessage>> RegisterErrorMessage<TError>(
            this IValidationErrorFormattingEngineBuilder<IEnumerable<IValidationErrorMessage>> builder,
            string code)
            where TError : class
        {
            builder.RegisterErrorOnlyFormatterFunc<TError>(_ => new ValidationErrorMessage { Code = code }.AsEnumerable());

            return builder;
        }
    }
}
