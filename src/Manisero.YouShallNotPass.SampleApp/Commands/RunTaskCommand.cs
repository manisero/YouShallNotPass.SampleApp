using Manisero.YouShallNotPass.SampleApp.Model;
using Manisero.YouShallNotPass.SampleApp.Validation.Rules;
using Manisero.YouShallNotPass.Validations;

namespace Manisero.YouShallNotPass.SampleApp.Commands
{
    public class RunTaskCommand : ICommand
    {
        public TaskConfiguration Configuration { get; set; }

        public static readonly IValidationRule<RunTaskCommand> ValidationRule = new ValidationRuleBuilder<RunTaskCommand>()
            .Member(x => x.Configuration,
            b => b.All(
                b1 => b1.NotNull(),
                _ => TaskConfigurationValidationRules.TaskConfiguration));
    }

    public class RunTaskCommandHandler : ICommandHandler<RunTaskCommand>
    {
        public void Handle(RunTaskCommand command)
        {
            // ...
        }
    }
}
