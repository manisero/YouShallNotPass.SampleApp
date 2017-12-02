using System.Web.Http;
using Manisero.YouShallNotPass.SampleApp.Commands;

namespace Manisero.YouShallNotPass.SampleApp.Web.Controllers
{
    public class TaskController : ControllerBase
    {
        public IHttpActionResult Post(RunTaskCommand command)
        {
            return HandleCommand(command);
        }
    }
}
