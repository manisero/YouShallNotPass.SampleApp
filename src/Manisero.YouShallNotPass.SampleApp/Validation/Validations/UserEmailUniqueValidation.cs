using Manisero.YouShallNotPass.SampleApp.Repositories;

namespace Manisero.YouShallNotPass.SampleApp.Validation.Validations
{
    public static class UserEmailUniqueValidation
    {
        public const string Code = "UserEmailUnique";

        public struct Input
        {
            public int? UserId { get; set; }
            public string Email { get; set; }

            public Input(string email)
            {
                UserId = null;
                Email = email;
            }

            public Input(int userId, string email)
            {
                UserId = userId;
                Email = email;
            }
        }

        public class Rule : IValidationRule<Input, Error>
        {
        }

        public class Error
        {
            public static readonly Error Instance = new Error();
        }

        public class Validator : IValidator<Rule, Input, Error>
        {
            private readonly IUserRepository _userRepository;

            public Validator(
                IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            public Error Validate(
                Input value,
                Rule rule,
                ValidationContext context)
            {
                var user = _userRepository.GetByEmail(value.Email);

                return user == null || value.UserId == user.UserId
                    ? null
                    : Error.Instance;
            }
        }
    }
}
