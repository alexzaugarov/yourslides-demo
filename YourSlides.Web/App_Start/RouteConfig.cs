using System.Web.Mvc;
using System.Web.Routing;

namespace YourSlides.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.RouteExistingFiles = false;
            routes.LowercaseUrls = true;
            routes.MapRoute(
                name: "GetSlideWithQuality",
                url: "Presentation/GetSlide/{presentationId}/{quality}/{slideIndex}",
                defaults: new {
                    controller = "Presentation",
                    action = "GetSlide",
                    presentationId = UrlParameter.Optional,
                    slideIndex = UrlParameter.Optional,
                    quality = UrlParameter.Optional
                }
            );
            routes.MapRoute(
                name: "GetSlide",
                url: "Presentation/GetSlide/{presentationId}/{slideIndex}",
                defaults:new{
                    controller = "Presentation", 
                    action = "GetSlide",
                    presentationId = UrlParameter.Optional,
                    slideIndex = UrlParameter.Optional
                }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
