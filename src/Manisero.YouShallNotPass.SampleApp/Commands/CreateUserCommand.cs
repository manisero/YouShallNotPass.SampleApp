using System.Collections.Generic;
using Manisero.YouShallNotPass.SampleApp.Model;
using Manisero.YouShallNotPass.SampleApp.Repositories;
using Manisero.YouShallNotPass.SampleApp.Validation.Rules;
using Manisero.YouShallNotPass.SampleApp.Validation.Validations;
using Manisero.YouShallNotPass.Validations;

namespace Manisero.YouShallNotPass.SampleApp.Commands
{
    public class CreateUserCommand : ICommand
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public static IValidationRule<CreateUserCommand> ValidationRule = new ComplexValidationRule<CreateUserCommand>
        {
            OverallRule = new AllValidationRule<CreateUserCommand>
            {
                Rules = new List<IValidationRule<CreateUserCommand>>
                {
                    new MemberValidationRule<CreateUserCommand, UserEmailUniqueValidationInput>
                    {
                        MemberName = nameof(Email),
                        ValueGetter = x => new UserEmailUniqueValidationInput(x.Email),
                        ValueValidationRule = new UserEmailUniqueValidationRule()
                    },
                    new MemberValidationRule<CreateUserCommand, UserEmailContainsLastNameValidationInput>
                    {
                        MemberName = nameof(Email),
                        ValueGetter = x => new UserEmailContainsLastNameValidationInput(x.Email, x.LastName),
                        ValueValidationRule = new UserEmailContainsLastNameValidationRule()
                    }
                }
            },
            MemberRules = new Dictionary<string, IValidationRule>
            {
                [nameof(Email)] = ValidationRules.UserEmailValidationRule,
                [nameof(FirstName)] = ValidationRules.UserFirstNameValidationRule,
                [nameof(LastName)] = ValidationRules.UserLastNameValidationRule
            }
        };
    }

    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(
            IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Handle(CreateUserCommand command)
        {
            var user = new User
            {
                Email = command.Email,
                FirstName = command.FirstName,
                LastName = command.LastName
            };

            _userRepository.Create(user);
        }
    }
}
