using System.Collections.Generic;
using Manisero.YouShallNotPass.ErrorFormatting;
using Manisero.YouShallNotPass.SampleApp.Utils;
using Manisero.YouShallNotPass.SampleApp.Validation.Validations;
using Manisero.YouShallNotPass.SampleApp.Validation.Validations.BuiltIn;
using Manisero.YouShallNotPass.SampleApp.Validation.Validations.Generic;
using Manisero.YouShallNotPass.Validations;

namespace Manisero.YouShallNotPass.SampleApp.Validation.Factories
{
    public class ValidationErrorFormattingEngineFactory
    {
        public IValidationErrorFormattingEngine<IEnumerable<IValidationErrorMessage>> Create()
        {
            var builder = new ValidationErrorFormattingEngineBuilder<IEnumerable<IValidationErrorMessage>>();

            // Built in
            builder.RegisterErrorOnlyFormatter(new AllValidationErrorFormatter());
            builder.RegisterErrorOnlyFormatter(new CollectionValidationErrorFormatter());
            builder.RegisterErrorMessage<EmailValidation.Error>(BuiltInValidationCodes.Email);
            builder.RegisterErrorOnlyFormatter(new IfValidationErrorFormatter());
            builder.RegisterErrorOnlyFormatter(new MapValidationErrorFormatter());
            builder.RegisterFullGenericFormatter(typeof(MemberValidationErrorFormatter<,>));
            builder.RegisterErrorOnlyGenericFormatter(typeof(MinValidationErrorFormatter<>));
            builder.RegisterErrorMessage<NotNullValidation.Error>(BuiltInValidationCodes.NotNull);
            builder.RegisterErrorMessage<NotNullNorWhiteSpaceValidation.Error>(BuiltInValidationCodes.NotNullNorWhiteSpace);

            // Custom (generic)
            builder.RegisterErrorMessage<BetweenValidation.Error>(BetweenValidation.Code);
            builder.RegisterErrorMessage<IsEnumValueValidation.Error>(IsEnumValueValidation.Code);
            builder.RegisterErrorMessage<NullValidation.Error>(NullValidation.Code);

            // Custom (specific)
            builder.RegisterErrorMessage<Algorithm4ConfigurationPhasesKeysValidation.Error>(Algorithm4ConfigurationPhasesKeysValidation.Code);
            builder.RegisterErrorMessage<UserEmailContainsLastNameValidation.Error>(UserEmailContainsLastNameValidation.Code);
            builder.RegisterErrorMessage<UserEmailUniqueValidation.Error>(UserEmailUniqueValidation.Code);
            builder.RegisterErrorMessage<UserExistsValidation.Error>(UserExistsValidation.Code);

            return builder.Build();
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
