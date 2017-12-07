using System.Collections.Generic;
using Manisero.YouShallNotPass.SampleApp.Utils;
using Manisero.YouShallNotPass.SampleApp.Validation.Validations.BuiltIn;

namespace Manisero.YouShallNotPass.SampleApp.Validation.MergingMemberErrors
{
    public class MemberErrorsMerger
    {
        private class MergedError
        {
            private readonly ICollection<IValidationErrorMessage> _selfErrors = new List<IValidationErrorMessage>();
            private readonly IDictionary<string, MergedError> _childErrors = new Dictionary<string, MergedError>();

            public void Add(IValidationErrorMessage error)
            {
                _selfErrors.Add(error);
            }

            public MergedError GetChildError(string member)
            {
                return _childErrors.GetOrAdd(member, _ => new MergedError());
            }

            public ICollection<IValidationErrorMessage> ToErrorMessages()
            {
                var result = new List<IValidationErrorMessage>(_selfErrors);

                foreach (var memberNameToError in _childErrors)
                {
                    result.Add(new MemberValidationErrorMessage
                    {
                        MemberName = memberNameToError.Key,
                        Errors = memberNameToError.Value.ToErrorMessages()
                    });
                }

                return result;
            }
        }

        public static ICollection<IValidationErrorMessage> Merge(ICollection<IValidationErrorMessage> errors)
        {
            var mergedError = new MergedError();
            MergeInto(errors, mergedError);

            return mergedError.ToErrorMessages();
        }

        private static void MergeInto(ICollection<IValidationErrorMessage> errors, MergedError mergedErrorToFill)
        {
            foreach (var error in errors)
            {
                if (error.Code.EqualsOrdinalIgnoreCase(BuiltInValidationCodes.Member))
                {
                    var memberError = (MemberValidationErrorMessage)error;
                    var childError = mergedErrorToFill.GetChildError(memberError.MemberName);
                    MergeInto(memberError.Errors, childError);
                }
                else if (error.Code.EqualsOrdinalIgnoreCase(BuiltInValidationCodes.Collection))
                {
                    var collectionError = (CollectionValidationErrorMessage)error;

                    foreach (var indexToItemError in collectionError.Errors)
                    {
                        var childError = mergedErrorToFill.GetChildError(indexToItemError.Key.ToString());
                        MergeInto(indexToItemError.Value, childError);
                    }
                } // TODO: Dictionary
                else
                {
                    mergedErrorToFill.Add(error);
                }
            }
        }
    }
}
