using System.Collections.Generic;
using Manisero.YouShallNotPass.ErrorFormatting;
using Manisero.YouShallNotPass.SampleApp.Validation.Validations;
using Manisero.YouShallNotPass.Validations;

namespace Manisero.YouShallNotPass.SampleApp.Validation.ValidationErrorFormatting
{
    public class ComplexValidationErrorMessage : IValidationErrorMessage
    {
        public string Code => BuiltInValidationCodes.Complex;
        public IEnumerable<IValidationErrorMessage> OverallError { get; set; }
        public IDictionary<string, IEnumerable<IValidationErrorMessage>> MemberErrors { get; set; } = new Dictionary<string, IEnumerable<IValidationErrorMessage>>();
    }

    public class ComplexValidationErrorFormatter<TValue> : IValidationErrorFormatter<ComplexValidationRule<TValue>,
                                                           TValue,
                                                           ComplexValidationError,
                                                           IEnumerable<IValidationErrorMessage>>
    {
        public IEnumerable<IValidationErrorMessage> Format(
            ValidationResult<ComplexValidationRule<TValue>, TValue, ComplexValidationError> validationResult,
            ValidationErrorFormattingContext<IEnumerable<IValidationErrorMessage>> context)
        {
            var error = validationResult.Error;
            var result = new ComplexValidationErrorMessage();

            if (error.OverallValidationError != null)
            {
                result.OverallError = context.Engine.Format(error.OverallValidationError);
            }

            if (error.MemberValidationErrors != null)
            {
                foreach (var memberValidationError in error.MemberValidationErrors)
                {
                    result.MemberErrors.Add(memberValidationError.Key, context.Engine.Format(memberValidationError.Value));
                }
            }

            yield return result;
        }
    }
}
