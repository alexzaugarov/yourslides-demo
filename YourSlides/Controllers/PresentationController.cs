using System.Web.Mvc;

namespace YourSlides.Controllers {
    public class PresentationController : Controller {
        public ActionResult View(long id) {
            var result = Json(new { newid = id });
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }

        public ActionResult Edit(long id) {
            throw new System.NotImplementedException();
        }
    }
}