using System.Collections.Generic;
using System.Linq;
using Manisero.YouShallNotPass.ErrorFormatting;
using Manisero.YouShallNotPass.Validations;

namespace Manisero.YouShallNotPass.SampleApp.Validation.Validations.BuiltIn
{
    public class AllValidationErrorFormatter<TValue> : IValidationErrorFormatter<AllValidation.Rule<TValue>,
                                                                                 TValue,
                                                                                 AllValidation.Error,
                                                                                 IEnumerable<IValidationErrorMessage>>
    {
        public IEnumerable<IValidationErrorMessage> Format(
            ValidationResult<AllValidation.Rule<TValue>, TValue, AllValidation.Error> validationResult,
            ValidationErrorFormattingContext<IEnumerable<IValidationErrorMessage>> context)
        {
            return validationResult.Error
                                   .Violations.Values
                                   .SelectMany(x => context.Engine.Format(x));
        }
    }
}
