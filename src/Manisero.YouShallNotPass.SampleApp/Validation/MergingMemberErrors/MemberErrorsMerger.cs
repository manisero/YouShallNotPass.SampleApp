using System.Collections.Generic;
using Manisero.YouShallNotPass.SampleApp.Utils;
using Manisero.YouShallNotPass.SampleApp.Validation.Validations.BuiltIn;
using Manisero.YouShallNotPass.SampleApp.Validation.Validations.Generic;

namespace Manisero.YouShallNotPass.SampleApp.Validation.MergingMemberErrors
{
    public class MemberErrorsMerger
    {
        private class MergedError
        {
            private readonly ICollection<IValidationErrorMessage> _selfErrors = new List<IValidationErrorMessage>();
            private readonly IDictionary<string, MergedError> _memberErrors = new Dictionary<string, MergedError>();
            private readonly IDictionary<int, MergedError> _itemErrors = new Dictionary<int, MergedError>();

            public void Add(IValidationErrorMessage error)
            {
                _selfErrors.Add(error);
            }

            public MergedError GetMemberError(string memberName)
            {
                return _memberErrors.GetOrAdd(memberName, _ => new MergedError());
            }

            public MergedError GetItemError(int itemIndex)
            {
                return _itemErrors.GetOrAdd(itemIndex, _ => new MergedError());
            }

            public ICollection<IValidationErrorMessage> ToErrorMessages()
            {
                var result = new List<IValidationErrorMessage>(_selfErrors);

                foreach (var memberNameToError in _memberErrors)
                {
                    result.Add(new MemberValidationErrorMessage
                    {
                        MemberName = memberNameToError.Key,
                        Errors = memberNameToError.Value.ToErrorMessages()
                    });
                }

                foreach (var itemIndexToError in _itemErrors)
                {
                    result.Add(new ItemValidationErrorMessage
                    {
                        ItemIndex = itemIndexToError.Key,
                        Errors = itemIndexToError.Value.ToErrorMessages()
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
            foreach (var errorMessage in errors)
            {
                if (errorMessage.Code.EqualsOrdinalIgnoreCase(BuiltInValidationCodes.Member))
                {
                    var memberErrorMessage = (MemberValidationErrorMessage)errorMessage;
                    var memberError = mergedErrorToFill.GetMemberError(memberErrorMessage.MemberName);
                    MergeInto(memberErrorMessage.Errors, memberError);
                }
                else if (errorMessage.Code.EqualsOrdinalIgnoreCase(BuiltInValidationCodes.Collection))
                {
                    var collectionErrorMessage = (CollectionValidationErrorMessage)errorMessage;

                    foreach (var indexToItemErrorMessage in collectionErrorMessage.Errors)
                    {
                        var itemError = mergedErrorToFill.GetItemError(indexToItemErrorMessage.Key);
                        MergeInto(indexToItemErrorMessage.Value, itemError);
                    }
                } // TODO: Dictionary
                else
                {
                    mergedErrorToFill.Add(errorMessage);
                }
            }
        }
    }
}
