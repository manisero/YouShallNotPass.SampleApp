using System.Net;
using System.Web.Http;

namespace Manisero.YouShallNotPass.SampleApp.Web.Controllers
{
    public class ControllerBase : ApiController
    {
        protected IHttpActionResult HandleCommand<TCommand>(TCommand command)
            where TCommand : ICommand
        {
            var result = AppGateway.Instance.HandleCommand(command);

            if (result.Success())
            {
                return Ok();
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result.ValidationError);
            }
        }

        protected TResult HandleQuery<TQuery, TResult>(TQuery query)
            where TQuery : IQuery<TResult>
        {
            return AppGateway.Instance.HandleQuery<TQuery, TResult>(query);
        }
    }
}
