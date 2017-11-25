using System.Collections.Generic;
using Manisero.YouShallNotPass.ErrorFormatting;
using Manisero.YouShallNotPass.Validations;

namespace Manisero.YouShallNotPass.SampleApp.Validation.ValidationErrorFormatting
{
    public class ComplexValidationErrorMessage
    {
        public string Code => "Complex";
        public IDictionary<string, object> MemberErrors { get; set; } = new Dictionary<string, object>();
        public object OverallError { get; set; }
    }

    public class ComplexValidationErrorFormatter<TValue> : IValidationErrorFormatter<ComplexValidationRule<TValue>, TValue, ComplexValidationError, object>
    {
        public object Format(
            ValidationResult<ComplexValidationRule<TValue>, TValue, ComplexValidationError> validationResult,
            ValidationErrorFormattingContext<object> context)
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

            return result;
        }
    }
}
