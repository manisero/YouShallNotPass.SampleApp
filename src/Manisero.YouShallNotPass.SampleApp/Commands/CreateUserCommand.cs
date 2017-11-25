namespace Manisero.YouShallNotPass.SampleApp.Commands
{
    public class CreateUserCommand
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class CreateUserCommandHandler : ICommandHanlder<CreateUserCommand>
    {
        public void Handle(CreateUserCommand command)
        {
            // Application logic...
        }
    }
}
