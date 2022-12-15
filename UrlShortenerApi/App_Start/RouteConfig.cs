using System.Web.Mvc;
using System.Web.Routing;

namespace UrlShortenerApi
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute("Error", "Error", defaults: new { controller = "Error", action = "BadRequest" });

            // Must come first
            routes.MapRoute(
                name: "Click",
                url: "{token}",
                defaults: new { controller = "Url", action = "Click" });


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Url", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
