using System.Collections.Generic;

namespace Manisero.YouShallNotPass.SampleApp.Validation.Validations.Generic
{
    public class ItemValidationErrorMessage : IValidationErrorMessage
    {
        public string Code => "Item";

        public int ItemIndex { get; set; }

        public ICollection<IValidationErrorMessage> Errors { get; set; }
    }
}
