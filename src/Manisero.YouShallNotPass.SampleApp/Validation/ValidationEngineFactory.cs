using Manisero.YouShallNotPass.SampleApp.Commands;

namespace Manisero.YouShallNotPass.SampleApp.Validation
{
    public class ValidationEngineFactory
    {
        public IValidationEngine Create()
        {
            return new ValidationEngineBuilder()
                .RegisterValidationRule(typeof(CreateUserCommand), CreateUserCommand.ValidationRule)
                .Build();
        }
    }
}
