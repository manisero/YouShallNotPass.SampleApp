using System.Web.Http;
using Manisero.YouShallNotPass.SampleApp.Commands;

namespace Manisero.YouShallNotPass.SampleApp.Web.Controllers
{
    public class UserController : ApiController
    {
        public object Post(CreateUserCommand command)
        {
            var result = AppGateway.Instance.Handle(command);

            return result;
        }
    }
}
