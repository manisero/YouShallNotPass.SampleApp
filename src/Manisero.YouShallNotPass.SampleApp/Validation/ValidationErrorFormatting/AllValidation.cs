using System.Collections.Generic;
using Manisero.YouShallNotPass.ErrorFormatting;
using Manisero.YouShallNotPass.Validations;

namespace Manisero.YouShallNotPass.SampleApp.Validation.ValidationErrorFormatting
{
    public class AllValidationErrorMessage
    {
        public string Code => "All";
        public ICollection<object> Errors { get; set; } = new List<object>();
    }

    public class AllValidationErrorFormatter<TValue> : IValidationErrorFormatter<AllValidationRule<TValue>, TValue, AllValidationError, object>
    {
        public object Format(
            ValidationResult<AllValidationRule<TValue>, TValue, AllValidationError> validationResult,
            ValidationErrorFormattingContext<object> context)
        {
            var error = validationResult.Error;
            var result = new AllValidationErrorMessage();

            if (error.Violations != null)
            {
                foreach (var violation in error.Violations)
                {
                    result.Errors.Add(context.Engine.Format(violation.Value));
                }
            }

            return result;
        }
    }
}
