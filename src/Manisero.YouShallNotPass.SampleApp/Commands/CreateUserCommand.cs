using System.Collections.Generic;
using Manisero.YouShallNotPass.Validations;

namespace Manisero.YouShallNotPass.SampleApp.Commands
{
    public class CreateUserCommand
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public static ComplexValidationRule<CreateUserCommand> ValidationRule = new ComplexValidationRule<CreateUserCommand>
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
        public void Handle(CreateUserCommand command)
        {
            // Application logic...
        }
    }
}
