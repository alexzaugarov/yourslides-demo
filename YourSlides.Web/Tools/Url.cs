using System;
using System.IO;
using System.Text;
using System.Web;

namespace YourSlides.Web.Tools {
    public class Url {
        public static string SlideUrlOrDefault(long id, int slideIndex, string preview = null) {
            var str = new StringBuilder("/storage/").Append(id).Append("/");
            if (!String.IsNullOrEmpty(preview)) {
                str.Append(preview).Append("/");
            }
            str.Append(slideIndex)
                .Append(".png");
            var imageurl = str.ToString();
            if (!File.Exists(HttpContext.Current.Server.MapPath(imageurl))) {
                imageurl = "/Content/images/default-w.png";
            }
            return imageurl;
        }
    }
}