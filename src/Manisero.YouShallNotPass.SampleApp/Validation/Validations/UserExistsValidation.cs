using Manisero.YouShallNotPass.SampleApp.Repositories;

namespace Manisero.YouShallNotPass.SampleApp.Validation.Validations
{
    public class UserExistsValidationRule : IValidationRule<int, UserExistsValidationError>
    {
    }

    public class UserExistsValidationError
    {
        public static readonly UserExistsValidationError Instance = new UserExistsValidationError();
    }

    public class UserExistsValidator : IValidator<UserExistsValidationRule, int, UserExistsValidationError>
    {
        private readonly IUserRepository _userRepository;

        public UserExistsValidator(
            IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserExistsValidationError Validate(int value, UserExistsValidationRule rule, ValidationContext context)
        {
            var user = _userRepository.Get(value);

            return user == null
                ? UserExistsValidationError.Instance
                : null;
        }
    }
}
