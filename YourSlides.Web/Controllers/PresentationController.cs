using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Yourslides.FileHandler.Service;
using Yourslides.Model.Api;
using Yourslides.Service;
using Yourslides.Utils.Web;
using YourSlides.Web.Models;
using YourSlides.Web.Tools;

namespace YourSlides.Web.Controllers {
    public class PresentationController : Controller {
        private readonly IFileService _fileService;
        private readonly IPresentationService _presentationService;
        private readonly IMapper _mapper;

        public PresentationController(IFileService fileService, IPresentationService presentationService, IMapper mapper) {
            _fileService = fileService;
            _presentationService = presentationService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        public ActionResult Watch(string id) {
            string visitorId = Request.IsAuthenticated ? User.Identity.GetUserId() : Request.AnonymousID;
            var presentation = _presentationService.GetPresentationForWatch(Obfuscator.Deobfuscate(id), visitorId);
            var relatedPresentation = _presentationService.GetRelatedPresentations(presentation);
            Debug.WriteLine(Request.AnonymousID);
            var presentationView = new WatchPageViewModel {
                Presentation = _mapper.Map<PresentationApi>(presentation),
                RelatedPresentations = _mapper.Map<IEnumerable<PresentationApi>>(relatedPresentation),
                IsOwner = presentation != null && presentation.OwnerId == User.Identity.GetUserId()
            };
            _presentationService.Save();
            return View(presentationView);
        }

        [AllowAnonymous]
        public ActionResult Embed(string id) {
            var presentation = _presentationService.Get(Obfuscator.Deobfuscate(id), User.Identity.GetUserId());
            var presentationView = new WatchPageViewModel {
                Presentation = _mapper.Map<PresentationApi>(presentation)
            };
            return View(presentationView);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Edit(string id) {
            var presentation = _presentationService.Get(Obfuscator.Deobfuscate(id), User.Identity.GetUserId(), Permission.High);
            var m = _mapper.Map<PresentationApi>(presentation);
            return View(m);
        }
        /*[Authorize]
        [HttpPost]
        public ActionResult Edit(PresentationApi p) {
            if (ModelState.IsValid) {
                var pId = _presentationService.Get(Obfuscator.Deobfuscate(p.Id), User.Identity.GetUserId(), Permission.High);
                try {
                    if (pId == null) {
                        return Json(new { success = false });
                    }
                    var result = _presentationService.UpdateAfterEdit(_mapper.Map<Presentation>(p));
                    if (result) {
                        _presentationService.Save();
                    }
                    return Json(new { success = result });
                } catch (Exception exception) {
                    Debug.WriteLine(exception.Message);
                }
            }
            return Json(new { success = false });
        }*/

        [Authorize]
        [HttpGet]
        public ActionResult Upload() {
            return View();
        }
        [HttpPost]
        [Authorize]
        public ActionResult UploadFile([ModelBinder(typeof(FileUploadModelBinder))]FilePresentation upload, string extraParam1 = null, int extraParam2 = 0) {
            var presentation = _presentationService.Create(Path.GetFileNameWithoutExtension(upload.Filename), User.Identity.GetUserId());
            try {
                _presentationService.Save();
                _fileService.SaveFile(upload, presentation);
                return new FileUploaderResult(true, new { url = Url.Action("Edit", new { Id = presentation.Id }) });
            } catch (Exception e) {
                string messageResult = "Unknown error";
                if (e is FileFormatNotSupportedException) {
                    messageResult = e.Message;
                }
                _presentationService.Delete(presentation);
                return new FileUploaderResult(false, messageResult);
            }
            //return new FileUploaderResult(true, new { url = Url.Action("Edit", new { Id = 12 }) });
        }

        public ActionResult GetSlide(long presentationId, int slideIndex, string quality = null) {
            var filePath = _fileService.GetSlide(presentationId, slideIndex, quality);
            return File(filePath, "image/jpeg");
        }
    }
}