using System.Collections.Generic;
using Manisero.YouShallNotPass.ErrorFormatting;
using Manisero.YouShallNotPass.SampleApp.Commands;
using Manisero.YouShallNotPass.SampleApp.Queries;
using Manisero.YouShallNotPass.SampleApp.Repositories;
using Manisero.YouShallNotPass.SampleApp.Validation;

namespace Manisero.YouShallNotPass.SampleApp
{
    public interface IAppGateway
    {
        CommandResult Handle<TCommand>(TCommand command)
            where TCommand : ICommand;

        TResult Handle<TQuery, TResult>(TQuery query)
            where TQuery : IQuery<TResult>;
    }

    public class AppGateway : IAppGateway
    {
        public static readonly AppGateway Instance = new AppGateway();

        private readonly IValidationEngine _validationEngine;
        private readonly IValidationErrorFormattingEngine<object> _validationErrorFormattingEngine;

        private readonly IDictionary<object, object> _commandHandlers;
        private readonly IDictionary<object, object> _queryHandlers;

        public AppGateway()
        {
            var userRepository = new UserRepository();

            _validationEngine = new ValidationEngineFactory().Create(userRepository);
            _validationErrorFormattingEngine = new ValidationErrorFormattingEngineFactory().Create();

            _commandHandlers = new Dictionary<object, object>
            {
                [typeof(CreateUserCommand)] = new CreateUserCommandHandler(userRepository),
                [typeof(UpdateUserCommand)] = new UpdateUserCommandHandler(userRepository)
            };

            _queryHandlers = new Dictionary<object, object>
            {
                [typeof(UsersQuery)] = new UsersQueryHandler(userRepository),
                [typeof(UserQuery)] = new UserQueryHandler(userRepository)
            };
        }

        public CommandResult Handle<TCommand>(TCommand command)
            where TCommand : ICommand
        {
            return ValidateCommand(command) ??
                   HandleCommand(command);
        }

        public TResult Handle<TQuery, TResult>(TQuery query)
            where TQuery : IQuery<TResult>
        {
            var handler = (IQueryHandler<TQuery, TResult>)_queryHandlers[typeof(TQuery)];

            return handler.Handle(query);
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
            where TCommand : ICommand
        {
            var handler = (ICommandHandler<TCommand>)_commandHandlers[typeof(TCommand)];
            handler.Handle(command);

            return new CommandResult();
        }
    }
}
