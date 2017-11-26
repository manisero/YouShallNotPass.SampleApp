using System.Collections.Generic;
using System.Linq;
using Manisero.YouShallNotPass.ErrorFormatting;
using Manisero.YouShallNotPass.Validations;

namespace Manisero.YouShallNotPass.SampleApp.Validation.Validations.BuiltIn
{
    public class ComplexValidationErrorMessage : IValidationErrorMessage
    {
        public string Code => BuiltInValidationCodes.Complex;

        public ICollection<IValidationErrorMessage> OverallError { get; set; }

        public IDictionary<string, ICollection<IValidationErrorMessage>> MemberErrors { get; set; } = new Dictionary<string, ICollection<IValidationErrorMessage>>();
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

            if (error.OverallViolation != null)
            {
                result.OverallError = context.Engine.Format(error.OverallViolation).ToArray();
            }

            if (error.MemberViolations != null)
            {
                foreach (var memberValidationError in error.MemberViolations)
                {
                    result.MemberErrors.Add(memberValidationError.Key,
                                            context.Engine.Format(memberValidationError.Value).ToArray());
                }
            }

            yield return result;
        }
    }
}
