using Manisero.YouShallNotPass.SampleApp.Model;

namespace Manisero.YouShallNotPass.SampleApp.Commands
{
    public class RunTaskCommand : ICommand
    {
        public TaskConfiguration Configuration { get; set; }
    }

    public class RunTaskCommandHandler : ICommandHandler<RunTaskCommand>
    {
        public void Handle(RunTaskCommand command)
        {
            // ...
        }
    }
}
