using System;
using System.Collections.Generic;
using Manisero.YouShallNotPass.SampleApp.Utils;
using Manisero.YouShallNotPass.SampleApp.Validation.Validations.BuiltIn;

namespace Manisero.YouShallNotPass.SampleApp.Validation.MergingMemberErrors
{
    public class ComplexValidationError
    {
        public ICollection<IValidationErrorMessage> OverallErrors { get; set; }

        public IDictionary<string, ICollection<IValidationErrorMessage>> MemberErrors { get; set; }
    }

    public interface IComplexValidationErrorBuilder
    {
        ComplexValidationError Build(ICollection<IValidationErrorMessage> errorMessages);
    }

    public class ComplexValidationErrorBuilder : IComplexValidationErrorBuilder
    {
        public ComplexValidationError Build(ICollection<IValidationErrorMessage> errorMessages)
        {
            var result = new ComplexValidationError
            {
                OverallErrors = new List<IValidationErrorMessage>(),
                MemberErrors = new Dictionary<string, ICollection<IValidationErrorMessage>>(StringComparer.OrdinalIgnoreCase)
            };

            foreach (var errorMessage in errorMessages)
            {
                if (errorMessage.Code.EqualsOrdinalIgnoreCase(BuiltInValidationCodes.Member))
                {
                    var memberErrorMessage = (MemberValidationErrorMessage)errorMessage;
                    var memberErrors = result.MemberErrors.GetOrAdd(memberErrorMessage.MemberName,
                                                                    _ => new List<IValidationErrorMessage>());

                    foreach (var memberError in memberErrorMessage.Errors)
                    {
                        memberErrors.Add(memberError);
                    }
                }
                else if (errorMessage.Code.EqualsOrdinalIgnoreCase(BuiltInValidationCodes.Collection))
                {
                    var collectionErrorMessage = (CollectionValidationErrorMessage)errorMessage;

                    foreach (var indexToErrors in collectionErrorMessage.Errors)
                    {
                        var memberName = indexToErrors.Key.ToString();
                        var indexErrors = result.MemberErrors.GetOrAdd(memberName,
                                                                       _ => new List<IValidationErrorMessage>());

                        foreach (var indexError in indexToErrors.Value)
                        {
                            indexErrors.Add(indexError);
                        }
                    }
                }
                else
                {
                    result.OverallErrors.Add(errorMessage);
                }
            }

            return result;
        }
    }
}
