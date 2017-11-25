using Manisero.YouShallNotPass.SampleApp.Repositories;

namespace Manisero.YouShallNotPass.SampleApp.Validation.Validations
{
    public class UserEmailUniqueValidationRule : IValidationRule<string, UserEmailUniqueValidationError>
    {
    }

    public class UserEmailUniqueValidationError
    {
        public const string Code = "UserEmailUnique";
        public static readonly UserEmailUniqueValidationError Instance = new UserEmailUniqueValidationError();
    }

    public class UserEmailUniqueValidator : IValidator<UserEmailUniqueValidationRule, string, UserEmailUniqueValidationError>
    {
        private readonly IUserRepository _userRepository;

        public UserEmailUniqueValidator(
            IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserEmailUniqueValidationError Validate(string value, UserEmailUniqueValidationRule rule, ValidationContext context)
        {
            // TODO: For UpdateUserCommand this will return error if command does not change Email. Fix this.

            var user = _userRepository.GetByEmail(value);

            return user != null
                ? UserEmailUniqueValidationError.Instance
                : null;
        }
    }
}
