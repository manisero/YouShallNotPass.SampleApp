using System.Collections.Generic;
using Manisero.YouShallNotPass.SampleApp.Model;

namespace Manisero.YouShallNotPass.SampleApp.Queries
{
    public class UserQuery : IQuery<ICollection<User>>
    {
        public int UserId { get; set; }
    }

    public class UserQueryHandler : IQueryHandler<UserQuery, ICollection<User>>
    {
        public ICollection<User> Handle(UserQuery query)
        {
            throw new System.NotImplementedException();
        }
    }
}
