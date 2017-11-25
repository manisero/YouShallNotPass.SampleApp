using System.Web.Http;

namespace Manisero.YouShallNotPass.SampleApp.Web.Controllers
{
    public class DemoController : ApiController
    {
        private static readonly IAppGateway _appGateway = new AppGateway();

        public object Get()
        {
            return 1;
        }
    }
}
