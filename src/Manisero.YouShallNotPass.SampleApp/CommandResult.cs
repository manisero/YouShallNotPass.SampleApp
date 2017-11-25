namespace Manisero.YouShallNotPass.SampleApp
{
    public class CommandResult
    {
        public object ValidationError { get; set; }
    }

    public static class CommandResultExtensions
    {
        public static bool Success(this CommandResult result) => result.ValidationError == null;
    }
}
