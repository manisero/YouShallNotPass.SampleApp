using System.Collections.Generic;
using System.Web.Http;
using Manisero.YouShallNotPass.SampleApp.Commands;
using Manisero.YouShallNotPass.SampleApp.Model;
using Manisero.YouShallNotPass.SampleApp.Queries;

namespace Manisero.YouShallNotPass.SampleApp.Web.Controllers
{
    public class UserController : ControllerBase
    {
        public object Get()
        {
            var query = new UsersQuery();

            return HandleQuery<UsersQuery, ICollection<User>>(query);
        }

        public IHttpActionResult Post(CreateUserCommand command)
        {
            return HandleCommand(command);
        }

        public IHttpActionResult Put(UpdateUserCommand command)
        {
            return HandleCommand(command);
        }
    }
}
