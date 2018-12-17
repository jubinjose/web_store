using Store.Api.Core;
using Store.Service;
using System.Web.Http;

namespace Store.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            TokenHelper.Init(ConfigService.GetInstance().GetJwtKey());
        }
    }
}
