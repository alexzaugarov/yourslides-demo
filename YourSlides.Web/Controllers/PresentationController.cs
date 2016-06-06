using System;
using System.IO;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Yourslides.FileHandler.Service;
using Yourslides.Model;
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
            var presentation = _presentationService.Get(Obfuscator.Deobfuscate(id));
            var presentationView = _mapper.Map<PresentationViewModel>(presentation);
            return View(presentationView);
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
        public FileUploaderResult UploadFile([ModelBinder(typeof(FileUploadModelBinder))]FilePresentation upload, string extraParam1 = null, int extraParam2 = 0) {
            var presentation = _presentationService.Create(Path.GetFileNameWithoutExtension(upload.Filename), User.Identity.GetUserId());
            try {
                _presentationService.Save();
               _fileService.SaveFile(upload, presentation);
               return new FileUploaderResult(true, presentation);
            } catch (Exception e) {
                string messageResult = "Unknown error";
                if(e is FileFormatNotSupportedException) {
                    messageResult = e.Message;
                }
                _presentationService.Delete(presentation);
                return new FileUploaderResult(false, messageResult);
            }
        }

        public ActionResult GetSlide(long presentationId, int slideIndex, string quality = null) {
            var filePath = _fileService.GetSlide(presentationId, slideIndex, quality);
            return File(filePath, "image/jpeg");
        }
    }
}