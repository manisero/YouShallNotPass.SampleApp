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
        public void Handle(UpdateUserCommand command)
        {
            // Application logic...
        }
    }
}
