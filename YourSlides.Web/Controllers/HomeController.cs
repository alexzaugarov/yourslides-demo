using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Yourslides.Model;
using Yourslides.Service;
using Yourslides.Utils.DateTimeUtils;
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
        public ActionResult Index() {
            var presentations = _presentationService.GetPublicPresentationsSubset(10, 0);
            var m = new MainPageModel {
                Presentations = _mapper.Map<IList<PresentationViewModel>>(presentations)
            };
            return View(m);
        }
    }
}