using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Yourslides.Model.Api;
using Yourslides.Model.SelectionOptions;
using Yourslides.Service;
using YourSlides.Web.Models;

namespace YourSlides.Web.Controllers {
    public class HomeController : Controller {
        private readonly IPresentationService _presentationService;
        private readonly IMapper _mapper;

        public HomeController(IPresentationService presentationService, IMapper mapper) {
            _presentationService = presentationService;
            _mapper = mapper;
        }

        // GET: Home
        public ActionResult Index(PresentationSelectionOptions options) {
            var presentations = _presentationService.Get(options, User.Identity.GetUserId());
            var m = new MainPageModel {
                Presentations = _mapper.Map<IList<PresentationApi>>(presentations),
                SearchOptions = options
            };
            return View(m);
        }
    }
}