using System.Collections.Generic;
using Manisero.YouShallNotPass.SampleApp.Model;
using Manisero.YouShallNotPass.SampleApp.Repositories;
using Manisero.YouShallNotPass.Validations;

namespace Manisero.YouShallNotPass.SampleApp.Commands
{
    public class CreateUserCommand
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public static IValidationRule<CreateUserCommand> ValidationRule = new ComplexValidationRule<CreateUserCommand>
        {
            MemberRules = new Dictionary<string, IValidationRule>
            {
                [nameof(Email)] = new AllValidationRule<string>
                {
                    Rules = new List<IValidationRule<string>>
                    {
                        new NotNullValidationRule<string>(),
                        new EmailValidationRule()
                    }
                },
                [nameof(FirstName)] = new NotNullNorWhiteSpaceValidationRule(),
                [nameof(LastName)] = new NotNullNorWhiteSpaceValidationRule()
            }
        };
    }

    public class CreateUserCommandHandler : ICommandHanlder<CreateUserCommand>
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
