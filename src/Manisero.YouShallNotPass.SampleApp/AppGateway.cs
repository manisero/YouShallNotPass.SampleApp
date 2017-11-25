using System.Collections.Generic;
using Manisero.YouShallNotPass.ErrorFormatting;
using Manisero.YouShallNotPass.SampleApp.Commands;
using Manisero.YouShallNotPass.SampleApp.Repositories;
using Manisero.YouShallNotPass.SampleApp.Validation;

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
        private readonly IValidationErrorFormattingEngine<object> _validationErrorFormattingEngine;

        private readonly IDictionary<object, object> _commandHandlers;

        public AppGateway()
        {
            _validationEngine = new ValidationEngineFactory().Create();
            _validationErrorFormattingEngine = new ValidationErrorFormattingEngineFactory().Create();

            var userRepository = new UserRepository();

            _commandHandlers = new Dictionary<object, object>
            {
                [typeof(CreateUserCommand)] = new CreateUserCommandHandler(userRepository),
                [typeof(UpdateUserCommand)] = new UpdateUserCommandHandler(userRepository)
            };
        }

        public CommandResult Handle<TCommand>(TCommand command)
        {
            return ValidateCommand(command) ??
                   HandleCommand(command);
        }

        private CommandResult ValidateCommand<TCommand>(TCommand command)
        {
            var validationResult = _validationEngine.TryValidate(command);

            if (validationResult == null || !validationResult.HasError())
            {
                return null;
            }

            var validationError = _validationErrorFormattingEngine.Format(validationResult);

            return new CommandResult
            {
                ValidationError = validationError
            };
        }

        private CommandResult HandleCommand<TCommand>(TCommand command)
        {
            var handler = (ICommandHanlder<TCommand>)_commandHandlers[typeof(TCommand)];
            handler.Handle(command);

            return new CommandResult();
        }
    }
}
