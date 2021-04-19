using EnglishBot.App_Start;
using EnglishBot.Models;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace EnglishBot
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected async void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            await Bot.Get();
        }
    }
}
