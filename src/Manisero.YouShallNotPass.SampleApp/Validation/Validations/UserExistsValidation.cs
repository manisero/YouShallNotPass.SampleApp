using Manisero.YouShallNotPass.SampleApp.Repositories;

namespace Manisero.YouShallNotPass.SampleApp.Validation.Validations
{
    public static class UserExistsValidation
    {
        public const string Code = "UserExists";

        public class Rule : IValidationRule<int, Error>
        {
        }

        public class Error
        {
            public static readonly Error Instance = new Error();
        }

        public class Validator : IValidator<Rule, int, Error>
        {
            private readonly IUserRepository _userRepository;

            public Validator(
                IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            public Error Validate(int value, Rule rule, ValidationContext context)
            {
                var user = _userRepository.Get(value);

                return user == null
                    ? Error.Instance
                    : null;
            }
        }
    }
}
