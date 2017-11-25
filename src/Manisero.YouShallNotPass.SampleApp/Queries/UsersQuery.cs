using System.Collections.Generic;
using Manisero.YouShallNotPass.SampleApp.Model;
using Manisero.YouShallNotPass.SampleApp.Repositories;

namespace Manisero.YouShallNotPass.SampleApp.Queries
{
    public class UsersQuery : IQuery<ICollection<User>>
    {
    }

    public class UsersQueryHandler : IQueryHandler<UsersQuery, ICollection<User>>
    {
        private readonly IUserRepository _userRepository;

        public UsersQueryHandler(
            IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ICollection<User> Handle(UsersQuery query)
        {
            return _userRepository.GetAll();
        }
    }
}
