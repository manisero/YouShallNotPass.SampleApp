using System.Collections.Generic;

namespace Manisero.YouShallNotPass.SampleApp.Validation.MergingMemberErrors
{
    public class ValidationError
    {
        public ICollection<IValidationErrorMessage> Original { get; set; }
        
        public ComplexValidationError Complex { get; set; }

        public ICollection<IValidationErrorMessage> Merged { get; set; }
    }

    public static class ValidationErrorBuilder
    {
        public static ValidationError Build(ICollection<IValidationErrorMessage> errorMessages)
        {
            return new ValidationError
            {
                Original = errorMessages,
                Complex = new ComplexValidationErrorBuilder().Build(errorMessages),
                Merged = MemberErrorsMerger.Merge(errorMessages)
            };
        }
    }
}
