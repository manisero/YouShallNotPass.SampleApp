using System.Collections.Generic;
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

        private readonly IValidationFacade _validationFacade;

        private readonly IDictionary<object, object> _commandHandlers;
        private readonly IDictionary<object, object> _queryHandlers;

        public AppGateway()
        {
            var userRepository = new UserRepository();

            _validationFacade = new ValidationFacade(userRepository);

            _commandHandlers = new Dictionary<object, object>
            {
                [typeof(CreateUserCommand)] = new CreateUserCommandHandler(userRepository),
                [typeof(UpdateUserCommand)] = new UpdateUserCommandHandler(userRepository)
            };

            _queryHandlers = new Dictionary<object, object>
            {
                [typeof(UsersQuery)] = new UsersQueryHandler(userRepository)
            };
        }

        public CommandResult Handle<TCommand>(TCommand command)
            where TCommand : ICommand
        {
            var validationError = _validationFacade.Validate(command);

            if (validationError != null)
            {
                return new CommandResult { ValidationError = validationError };
            }

            var handler = (ICommandHandler<TCommand>)_commandHandlers[typeof(TCommand)];
            handler.Handle(command);

            return new CommandResult();
        }

        public TResult Handle<TQuery, TResult>(TQuery query)
            where TQuery : IQuery<TResult>
        {
            var handler = (IQueryHandler<TQuery, TResult>)_queryHandlers[typeof(TQuery)];

            return handler.Handle(query);
        }
    }
}
