using System.Collections.Generic;
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

        public static IValidationRule<UpdateUserCommand> ValidationRule = new ComplexValidationRule<UpdateUserCommand>
        {
            OverallRule = new AllValidationRule<UpdateUserCommand>
            {
                Rules = new List<IValidationRule<UpdateUserCommand>>
                {
                    new MemberValidationRule<UpdateUserCommand, UserEmailUniqueValidationInput>
                    {
                        MemberName = nameof(Email),
                        ValueGetter = x => new UserEmailUniqueValidationInput(x.UserId, x.Email),
                        ValueValidationRule = new UserEmailUniqueValidationRule()
                    },
                    new MemberValidationRule<UpdateUserCommand, UserEmailContainsLastNameValidationInput>
                    {
                        MemberName = nameof(Email),
                        ValueGetter = x => new UserEmailContainsLastNameValidationInput(x.Email, x.LastName),
                        ValueValidationRule = new UserEmailContainsLastNameValidationRule()
                    }
                }
            },
            MemberRules = new Dictionary<string, IValidationRule>
            {
                [nameof(UserId)] = ValidationRules.UserIdValidationRule,
                [nameof(Email)] = ValidationRules.UserEmailValidationRule,
                [nameof(FirstName)] = ValidationRules.UserFirstNameValidationRule,
                [nameof(LastName)] = ValidationRules.UserLastNameValidationRule
            }
        };
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
