using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Snacks_R_Us.WebApp
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            RegisterRoutes(RouteTable.Routes);
        }

        private static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Default", "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = "" }  
            );
        }
    }
}