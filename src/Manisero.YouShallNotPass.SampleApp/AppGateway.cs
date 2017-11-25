using System.Collections.Generic;
using Manisero.YouShallNotPass.SampleApp.Commands;

namespace Manisero.YouShallNotPass.SampleApp
{
    public interface IAppGateway
    {
        CommandResult Handle<TCommand>(TCommand command);
    }

    public class AppGateway : IAppGateway
    {
        public static readonly AppGateway Instance = new AppGateway();

        private readonly IDictionary<object, object> _commandHandlers = new Dictionary<object, object>
        {
            [typeof(CreateUserCommand)] = new CreateUserCommandHandler()
        };

        public CommandResult Handle<TCommand>(TCommand command)
        {
            var handler = (ICommandHanlder<TCommand>)_commandHandlers[typeof(TCommand)];
            handler.Handle(command);

            return new CommandResult();
        }
    }
}
