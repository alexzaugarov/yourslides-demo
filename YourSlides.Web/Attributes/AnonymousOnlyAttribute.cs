using System.Web.Mvc;
using System.Web.Routing;

namespace YourSlides.Web.Attributes {
    public class AnonymousOnlyAttribute : AuthorizeAttribute {
        public override void OnAuthorization(AuthorizationContext filterContext) {
            if (filterContext.HttpContext.Request.IsAuthenticated) {
                // Do what you want to do here, e.g. show a 404 or redirect
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new {
                    action = "Index",
                    controller = "Home",
                    area = ""
                }));
            }
        }
    }
}