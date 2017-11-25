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

        private readonly IValidationEngine _validationEngine;

        private readonly IDictionary<object, object> _commandHandlers = new Dictionary<object, object>
        {
            [typeof(CreateUserCommand)] = new CreateUserCommandHandler()
        };

        public AppGateway()
        {
            _validationEngine = new ValidationEngineBuilder()
                .RegisterValidationRule(typeof(CreateUserCommand), CreateUserCommand.ValidationRule)
                .Build();
        }

        public CommandResult Handle<TCommand>(TCommand command)
        {
            var validationResult = _validationEngine.Validate(command);

            if (validationResult.HasError())
            {
                return new CommandResult
                {
                    ValidationError = validationResult.Error
                };
            }

            var handler = (ICommandHanlder<TCommand>)_commandHandlers[typeof(TCommand)];
            handler.Handle(command);

            return new CommandResult();
        }
    }
}
