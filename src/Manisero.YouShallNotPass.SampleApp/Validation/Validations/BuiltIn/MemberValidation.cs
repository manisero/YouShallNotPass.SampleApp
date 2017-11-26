using System.Collections.Generic;
using System.Linq;
using Manisero.YouShallNotPass.ErrorFormatting;
using Manisero.YouShallNotPass.Validations;

namespace Manisero.YouShallNotPass.SampleApp.Validation.Validations.BuiltIn
{
    public class MemberValidationErrorMessage : IValidationErrorMessage
    {
        public string Code => "Member";

        public string MemberName { get; set; }

        public ICollection<IValidationErrorMessage> Errors { get; set; }
    }

    public class MemberValidationErrorFormatter<TOwner, TValue> : IValidationErrorFormatter<MemberValidationRule<TOwner, TValue>,
                                                                                            TOwner,
                                                                                            MemberValidationError,
                                                                                            IEnumerable<IValidationErrorMessage>>
    {
        public IEnumerable<IValidationErrorMessage> Format(
            ValidationResult<MemberValidationRule<TOwner, TValue>, TOwner, MemberValidationError> validationResult,
            ValidationErrorFormattingContext<IEnumerable<IValidationErrorMessage>> context)
        {
            yield return new MemberValidationErrorMessage
            {
                MemberName = validationResult.Rule.MemberName,
                Errors = context.Engine.Format(validationResult.Error.Violation).ToArray()
            };
        }
    }
}
