using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Utils;
using YourSlides.Models;

namespace YourSlides.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var m = new MainPageModel {
                Presentations = new List<Presentation> {
                    new Presentation {
                        Id = 1,
                        OwnerName = "alex52711",
                        Title = "Presentation",
                        Created = UnixTime.UtcNow,
                        SlideCount = 10,
                        WatchCount = 1
                    }
                }
            };
            return View(m);
        }
    }
}