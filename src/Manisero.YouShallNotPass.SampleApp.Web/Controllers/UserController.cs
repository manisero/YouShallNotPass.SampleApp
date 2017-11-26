using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using Manisero.YouShallNotPass.SampleApp.Commands;
using Manisero.YouShallNotPass.SampleApp.Model;
using Manisero.YouShallNotPass.SampleApp.Queries;

namespace Manisero.YouShallNotPass.SampleApp.Web.Controllers
{
    public class UserController : ApiController
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

        private IHttpActionResult HandleCommand<TCommand>(TCommand command)
            where TCommand : ICommand
        {
            var result = AppGateway.Instance.Handle(command);

            if (result.Success())
            {
                return Ok();
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result.ValidationError);
            }
        }

        private TResult HandleQuery<TQuery, TResult>(TQuery query)
            where TQuery : IQuery<TResult>
        {
            return AppGateway.Instance.Handle<TQuery, TResult>(query);
        }
    }
}
