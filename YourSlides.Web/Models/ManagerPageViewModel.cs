using System.Collections.Generic;
using Yourslides.Model;
using Yourslides.Model.Api;

namespace YourSlides.Web.Models {
    public class ManagerPageViewModel : UserPageViewModel {
        public IDictionary<PresentationVisibility, int> VisibilityStats { get; set; }
        public IDictionary<PresentationStatus, int> StatusStats { get; set; }
    }
}