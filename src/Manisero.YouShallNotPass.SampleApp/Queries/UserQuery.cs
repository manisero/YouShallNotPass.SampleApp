using Manisero.YouShallNotPass.SampleApp.Model;
using Manisero.YouShallNotPass.SampleApp.Repositories;

namespace Manisero.YouShallNotPass.SampleApp.Queries
{
    public class UserQuery : IQuery<User>
    {
        public int UserId { get; set; }
    }

    public class UserQueryHandler : IQueryHandler<UserQuery, User>
    {
        private readonly IUserRepository _userRepository;

        public UserQueryHandler(
            IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User Handle(UserQuery query)
        {
            return _userRepository.Get(query.UserId);
        }
    }
}
