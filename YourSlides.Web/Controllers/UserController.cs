using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Yourslides.Model;
using Yourslides.Model.Account;
using Yourslides.Model.Api;
using Yourslides.Model.SelectionOptions;
using Yourslides.Service;
using YourSlides.Web.Models;

namespace YourSlides.Web.Controllers {
    public class UserController : Controller {
        private readonly IPresentationService _presentationService;
        private readonly IMapper _mapper;

        public UserController(IPresentationService presentationService, IMapper mapper) {
            _presentationService = presentationService;
            _mapper = mapper;
        }
        private UserManager<User> UserManager {
            get { return HttpContext.GetOwinContext().GetUserManager<UserManager<User>>(); }
        }
        public ActionResult Index(string userName = "") {
            var owner = UserManager.FindByName(userName);
            if (owner == null) {
                return Redirect("~");
            }
            var searchOptions = new PresentationSelectionOptions {
                Count = 15,
                OwnerId = owner.Id,
                Visibility = PresentationVisibility.All,
                Status = PresentationStatus.Ready
            };
            var presentations = _presentationService.Get(searchOptions, User.Identity.GetUserId());
            var m = new UserPageViewModel {
                Owner = owner,
                Presentations = _mapper.Map<IEnumerable<PresentationApi>>(presentations),
                SearchOptions = searchOptions
            };
            return View(m);
        }

        [Authorize]
        public ActionResult Manager() {
            var owner = UserManager.FindById(User.Identity.GetUserId());
            var options = new PresentationSelectionOptions {
                OwnerId = owner.Id,
                Count = 10
            };
            var result = _presentationService.Get(options, owner.Id);
            var m = new ManagerPageViewModel {
                Presentations = _mapper.Map<IEnumerable<PresentationApi>>(result),
                Owner = owner,
                SearchOptions = options,
            };
            if (m.Presentations.Any()) {
                m.StatusStats = _presentationService.GetStatusStats(owner.Id);
                m.VisibilityStats = _presentationService.GetVisibilityStats(owner.Id);
            }
            return View(m);
        }
    }
}