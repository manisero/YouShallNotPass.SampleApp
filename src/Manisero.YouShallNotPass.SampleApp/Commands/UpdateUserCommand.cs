using Manisero.YouShallNotPass.SampleApp.Model;
using Manisero.YouShallNotPass.SampleApp.Repositories;
using Manisero.YouShallNotPass.SampleApp.Validation.Rules;
using Manisero.YouShallNotPass.SampleApp.Validation.Validations;
using Manisero.YouShallNotPass.Validations;

namespace Manisero.YouShallNotPass.SampleApp.Commands
{
    public class UpdateUserCommand : ICommand
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public static readonly IValidationRule<UpdateUserCommand> ValidationRule = new ValidationRuleBuilder<UpdateUserCommand>()
            .All(b => b.Member(
                     nameof(Email),
                     x => new UserEmailUniqueValidation.Input(x.UserId, x.Email),
                     _ => new UserEmailUniqueValidation.Rule()),
                 b => b.Member(
                     nameof(Email),
                     x => new UserEmailContainsLastNameValidation.Input(x.Email, x.LastName),
                     _ => new UserEmailContainsLastNameValidation.Rule()),
                 b => b.Member(x => x.UserId, UserValidationRules.UserIdRule),
                 b => b.Member(x => x.Email, UserValidationRules.EmailRule),
                 b => b.Member(x => x.FirstName, UserValidationRules.FirstNameRule),
                 b => b.Member(x => x.LastName, UserValidationRules.LastNameRule));
    }

    public class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserCommandHandler(
            IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Handle(UpdateUserCommand command)
        {
            var user = new User
            {
                UserId = command.UserId,
                Email = command.Email,
                FirstName = command.FirstName,
                LastName = command.LastName
            };

            _userRepository.Put(user);
        }
    }
}
