using Manisero.YouShallNotPass.SampleApp.Model;
using Manisero.YouShallNotPass.SampleApp.Repositories;

namespace Manisero.YouShallNotPass.SampleApp.Commands
{
    public class UpdateUserCommand
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // TODO: Validation rule
    }

    public class UpdateUserCommandHandler : ICommandHanlder<UpdateUserCommand>
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
