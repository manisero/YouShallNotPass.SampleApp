using System;
using System.Collections.Generic;
using System.Linq;
using Manisero.YouShallNotPass.ErrorFormatting;
using Manisero.YouShallNotPass.SampleApp.Validation.ValidationErrorFormatting;

namespace Manisero.YouShallNotPass.SampleApp.Validation.Validations
{
    public class PropertyValidationRule<TOwner, TValue> : IValidationRule<TOwner, PropertyValidationError>
    {
        public string PropertyName { get; set; }

        public Func<TOwner, TValue> ValueGetter { get; set; }

        public IValidationRule<TValue> ValueValidationRule { get; set; }
    }

    public class PropertyValidationError
    {
        public IValidationResult Violation { get; set; }
    }

    public class PropertyValidator<TOwner, TValue> : IValidator<PropertyValidationRule<TOwner, TValue>, TOwner, PropertyValidationError>
    {
        public PropertyValidationError Validate(TOwner value, PropertyValidationRule<TOwner, TValue> rule, ValidationContext context)
        {
            var propertyValue = rule.ValueGetter(value);

            var validationResult = context.Engine.Validate(propertyValue, rule.ValueValidationRule);

            return validationResult.HasError()
                ? new PropertyValidationError { Violation = validationResult }
                : null;
        }
    }

    public class PropertyValidationErrorMessage : IValidationErrorMessage
    {
        public string Code => "Property";

        public string PropertyName { get; set; }

        public ICollection<IValidationErrorMessage> Errors { get; set; }
    }

    public class PropertyValidationErrorFormatter<TOwner, TValue> : IValidationErrorFormatter<PropertyValidationRule<TOwner, TValue>,
                                                                                              TOwner,
                                                                                              PropertyValidationError,
                                                                                              IEnumerable<IValidationErrorMessage>>
    {
        public IEnumerable<IValidationErrorMessage> Format(
            ValidationResult<PropertyValidationRule<TOwner, TValue>, TOwner, PropertyValidationError> validationResult,
            ValidationErrorFormattingContext<IEnumerable<IValidationErrorMessage>> context)
        {
            yield return new PropertyValidationErrorMessage
            {
                PropertyName = validationResult.Rule.PropertyName,
                Errors = context.Engine.Format(validationResult.Error.Violation).ToArray()
            };
        }
    }
}
