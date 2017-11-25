using System.Net;
using System.Web.Http;
using Manisero.YouShallNotPass.SampleApp.Commands;

namespace Manisero.YouShallNotPass.SampleApp.Web.Controllers
{
    public class UserController : ApiController
    {
        public IHttpActionResult Post(CreateUserCommand command)
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
    }
}
