using System.Collections.Generic;
using System.Linq;
using Manisero.YouShallNotPass.ErrorFormatting;
using Manisero.YouShallNotPass.Validations;

namespace Manisero.YouShallNotPass.SampleApp.Validation.Validations.BuiltIn
{
    public class AllValidationErrorFormatter<TValue> : IValidationErrorFormatter<AllValidationRule<TValue>,
                                                                                 TValue,
                                                                                 AllValidationError,
                                                                                 IEnumerable<IValidationErrorMessage>>
    {
        public IEnumerable<IValidationErrorMessage> Format(
            ValidationResult<AllValidationRule<TValue>, TValue, AllValidationError> validationResult,
            ValidationErrorFormattingContext<IEnumerable<IValidationErrorMessage>> context)
        {
            return validationResult.Error
                                   .Violations.Values
                                   .SelectMany(x => context.Engine.Format(x));
        }
    }
}
