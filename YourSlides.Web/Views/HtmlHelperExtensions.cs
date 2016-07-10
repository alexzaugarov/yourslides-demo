using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace YourSlides.Web.Views {
    public static class HtmlHelperExtensions {
        public static string UrlWithHost(this HtmlHelper htmlHelper, params string[] paths) {
            var host = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
            var list = new List<string>{host};
            list.AddRange(paths);
            return Url(htmlHelper, false, list.ToArray());
        }
        public static string Url(this HtmlHelper htmlHelper, bool isRoot, params string[] paths) {
            var result = new StringBuilder();
            if (isRoot) {
                result.Append("/");
            }
            for (int i = 0; i < paths.Length; i++) {
                result.Append(paths[i].Trim().TrimStart('/').TrimEnd('/'));
                if (i + 1 != paths.Length) {
                    result.Append("/");
                }
            }
            return result.ToString();
        }

        public static string RootUrl(this HtmlHelper htmlHelper, params string[] paths) {
            return Url(htmlHelper, true, paths);
        }
        public static string MyActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object routeValues, object htmlAttributes) {
            var tagActionLink = htmlHelper.ActionLink("[replace]", actionName, controllerName, routeValues, htmlAttributes).ToHtmlString();
            return tagActionLink.Replace("[replace]", linkText);
        }
    }
}