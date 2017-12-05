using Manisero.YouShallNotPass.SampleApp.Validation.MergingMemberErrors;

namespace Manisero.YouShallNotPass.SampleApp
{
    public class CommandResult
    {
        public ValidationError ValidationError { get; set; }
    }

    public static class CommandResultExtensions
    {
        public static bool Success(this CommandResult result) => result.ValidationError == null;
    }
}
