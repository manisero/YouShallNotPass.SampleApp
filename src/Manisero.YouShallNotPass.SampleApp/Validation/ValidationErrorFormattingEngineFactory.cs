using Manisero.YouShallNotPass.ErrorFormatting;
using Manisero.YouShallNotPass.SampleApp.Validation.ValidationErrorFormatting;
using Manisero.YouShallNotPass.Validations;

namespace Manisero.YouShallNotPass.SampleApp.Validation
{
    public class ValidationErrorFormattingEngineFactory
    {
        public IValidationErrorFormattingEngine<object> Create()
        {
            return new ValidationErrorFormattingEngineBuilder<object>()
                .RegisterFullGenericFormatter(typeof(AllValidationErrorFormatter<>))
                .RegisterFullGenericFormatter(typeof(ComplexValidationErrorFormatter<>))
                .RegisterErrorMessage<EmailValidationError>("Email")
                .Build();
        }
    }

    public static class ValidationErrorFormattingEngineBuilderExtensions
    {
        public static IValidationErrorFormattingEngineBuilder<object> RegisterErrorMessage<TError>(
            this IValidationErrorFormattingEngineBuilder<object> builder,
            string code)
            where TError : class
        {
            builder.RegisterErrorOnlyFormatterFunc<TError>(_ => new ValidationErrorMessage
            {
                Code = code
            });

            return builder;
        }
    }
}
