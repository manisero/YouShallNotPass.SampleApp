using Manisero.YouShallNotPass.SampleApp.Validation.ValidationErrorFormatting;

namespace Manisero.YouShallNotPass.SampleApp
{
    public class CommandResult
    {
        public IValidationErrorMessage ValidationError { get; set; }
    }

    public static class CommandResultExtensions
    {
        public static bool Success(this CommandResult result) => result.ValidationError == null;
    }
}
