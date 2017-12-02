using System.Collections.Generic;
using Manisero.YouShallNotPass.SampleApp.Validation;

namespace Manisero.YouShallNotPass.SampleApp
{
    public class CommandResult
    {
        public ICollection<IValidationErrorMessage> ValidationError { get; set; }
    }

    public static class CommandResultExtensions
    {
        public static bool Success(this CommandResult result) => result.ValidationError == null;
    }
}
