using System;
using System.Configuration;
using System.IO;
using System.Web.Mvc;
using Core.Utils;
using YourSlides.Models;
using YourSlides.Tools;

namespace YourSlides.Controllers {
    public class PresentationController : Controller {
        [AllowAnonymous]
        public ActionResult Watch(string id) {
            var p = new Presentation {
                Id = 1,
                OwnerName = "alex52711",
                Title = "Руководство пользователя Battlefield 3",
                Created = UnixTime.UtcNow,
                SlideCount = 19,
                WatchCount = 1
            };
            return View(p);
        }

        public ActionResult Edit(long id) {
            throw new System.NotImplementedException();
        }
        [Authorize]
        [HttpGet]
        public ActionResult Upload() {
            return View();
        }
        [HttpPost]
        [Authorize]
        public FineUploaderResult UploadFile(FineUpload upload, string extraParam1 = null, int extraParam2 = 0) {
            // asp.net mvc will set extraParam1 and extraParam2 from the params object passed by Fine-Uploader

            var dir = ConfigurationManager.AppSettings["PresentationStorageLocation"];
            var filePath = Path.Combine(dir, upload.Filename);
            try {
                upload.SaveAs(filePath);
            } catch (Exception ex) {
                return new FineUploaderResult(false, error: ex.Message);
            }

            // the anonymous object in the result below will be convert to json and set back to the browser
            return new FineUploaderResult(true, new { extraInformation = 12345 });
        }

        public ActionResult GetSlide(string presentationId, string slideName) {
            var filePath = ConfigurationManager.AppSettings["PresentationStorageLocation"] + presentationId + "/" + slideName + ".jpg";
            return File(filePath, "image/jpeg");
        }
    }
}