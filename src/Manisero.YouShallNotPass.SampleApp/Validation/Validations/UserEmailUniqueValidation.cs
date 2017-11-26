using Manisero.YouShallNotPass.SampleApp.Repositories;

namespace Manisero.YouShallNotPass.SampleApp.Validation.Validations
{
    public struct UserEmailUniqueValidationInput
    {
        public int? UserId { get; set; }
        public string Email { get; set; }

        public UserEmailUniqueValidationInput(string email)
        {
            UserId = null;
            Email = email;
        }

        public UserEmailUniqueValidationInput(int userId, string email)
        {
            UserId = userId;
            Email = email;
        }
    }

    public class UserEmailUniqueValidationRule : IValidationRule<UserEmailUniqueValidationInput, UserEmailUniqueValidationError>
    {
    }

    public class UserEmailUniqueValidationError
    {
        public const string Code = "UserEmailUnique";
        public static readonly UserEmailUniqueValidationError Instance = new UserEmailUniqueValidationError();
    }

    public class UserEmailUniqueValidator : IValidator<UserEmailUniqueValidationRule, UserEmailUniqueValidationInput, UserEmailUniqueValidationError>
    {
        private readonly IUserRepository _userRepository;

        public UserEmailUniqueValidator(
            IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserEmailUniqueValidationError Validate(
            UserEmailUniqueValidationInput value,
            UserEmailUniqueValidationRule rule,
            ValidationContext context)
        {
            var user = _userRepository.GetByEmail(value.Email);

            return user == null || value.UserId == user.UserId
                ? null
                : UserEmailUniqueValidationError.Instance;
        }
    }
}
