using System.Web;
using System.Web.Http;

namespace Manisero.YouShallNotPass.SampleApp.Web
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
