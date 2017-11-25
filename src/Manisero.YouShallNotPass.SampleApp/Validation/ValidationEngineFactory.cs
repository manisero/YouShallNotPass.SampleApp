using Manisero.YouShallNotPass.SampleApp.Commands;

namespace Manisero.YouShallNotPass.SampleApp.Validation
{
    public class ValidationEngineFactory
    {
        public IValidationEngine Create()
        {
            return new ValidationEngineBuilder()
                .RegisterValidationRule(typeof(CreateUserCommand), CreateUserCommand.ValidationRule)
                .RegisterValidationRule(typeof(UpdateUserCommand), UpdateUserCommand.ValidationRule)
                // TODO: Register UserId and Email validators
                .Build();
        }
    }
}
