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
                name: "Embedding",
                url: "embed/{id}",
                defaults: new {
                    controller = "Presentation",
                    action = "Embed"
                }
            );
            routes.MapRoute(
                name: "Manager",
                url: "manager",
                defaults: new {
                    controller = "User",
                    action = "Manager"
                }
            );
            routes.MapRoute(
                name: "UserPage",
                url: "user/{username}",
                defaults: new {
                    controller = "User",
                    action = "Index"
                }
            );
            routes.MapRoute(
                name: "Upload",
                url: "upload",
                defaults: new {
                    controller = "Presentation",
                    action = "Upload"
                }
            );
            routes.MapRoute(
                name: "Watch",
                url: "watch/{id}",
                defaults:new{
                    controller = "Presentation", 
                    action = "Watch"
                }
            );
            routes.MapRoute(
                name: "Edit",
                url: "edit/{id}",
                defaults:new{
                    controller = "Presentation", 
                    action = "Edit"
                }
            );
            routes.MapRoute(
                name: "Signin",
                url: "signin",
                defaults:new{
                    controller = "Auth", 
                    action = "Signin"
                }
            );
            routes.MapRoute(
                name: "Signout",
                url: "signout",
                defaults:new{
                    controller = "Auth", 
                    action = "Signout"
                }
            );
            routes.MapRoute(
                name: "Signup",
                url: "signup",
                defaults:new{
                    controller = "Auth", 
                    action = "Signup"
                }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional}
            );
        }
    }
}
