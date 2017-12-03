using System.Collections.Generic;
using System.Linq;
using Manisero.YouShallNotPass.ErrorFormatting;
using Manisero.YouShallNotPass.SampleApp.Utils;
using Manisero.YouShallNotPass.Validations;

namespace Manisero.YouShallNotPass.SampleApp.Validation.Validations.BuiltIn
{
    public class CollectionValidationErrorMessage : IValidationErrorMessage
    {
        public string Code => BuiltInValidationCodes.Collection;

        public IDictionary<int, ICollection<IValidationErrorMessage>> Errors { get; set; }
    }

    public class CollectionValidationErrorFormatter : IValidationErrorFormatter<CollectionValidation.Error,
                                                                                       IEnumerable<IValidationErrorMessage>>
    {
        public IEnumerable<IValidationErrorMessage> Format(
            CollectionValidation.Error error,
            ValidationErrorFormattingContext<IEnumerable<IValidationErrorMessage>> context)
        {
            return new CollectionValidationErrorMessage
            {
                Errors = error.Violations
                              .ToDictionary(x => x.Key,
                                            x => (ICollection<IValidationErrorMessage>)context.Engine.Format(x.Value).ToArray())
            }.AsEnumerable();
        }
    }
}
