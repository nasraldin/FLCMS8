using System.Web.Mvc;
using System.Web.Routing;

namespace FalMedia
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("DefaultLocalized",
                "{culture}/{controller}/{action}/{id}",
                new {controller = "Home", action = "Index", id = UrlParameter.Optional, culture = "en-us"});

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new {controller = "Home", action = "Index", id = UrlParameter.Optional}
            );
        }
    }
}