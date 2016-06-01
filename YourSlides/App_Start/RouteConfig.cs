using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace YourSlides
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.RouteExistingFiles = false;
            routes.MapRoute(
                name: "GetSlide",
                url: "Presentation/GetSlide/{presentationId}/{slideName}",
                defaults:new{
                    controller = "Presentation", 
                    action = "GetSlide",
                    presentationId = UrlParameter.Optional,
                    slideName = UrlParameter.Optional
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
