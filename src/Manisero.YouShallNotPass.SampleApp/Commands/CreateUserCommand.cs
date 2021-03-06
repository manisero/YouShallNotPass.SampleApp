﻿using Manisero.YouShallNotPass.SampleApp.Model;
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

        public static readonly IValidationRule<CreateUserCommand> ValidationRule = new ValidationRuleBuilder<CreateUserCommand>()
            .All(b => b.Member(
                     nameof(Email),
                     x => new UserEmailUniqueValidation.Input(x.Email),
                     new UserEmailUniqueValidation.Rule()),
                 b => b.Member(
                     nameof(Email),
                     x => new UserEmailContainsLastNameValidation.Input(x.Email, x.LastName),
                     new UserEmailContainsLastNameValidation.Rule()),
                 b => b.Member(x => x.Email, UserValidationRules.EmailRule),
                 b => b.Member(x => x.FirstName, UserValidationRules.FirstNameRule),
                 b => b.Member(x => x.LastName, UserValidationRules.LastNameRule));
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
