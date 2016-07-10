using System.Collections.Generic;
using Yourslides.Model;
using Yourslides.Model.Api;

namespace YourSlides.Web.Models {
    public class WatchPageViewModel {
        public PresentationApi Presentation { get; set; }
        public IEnumerable<PresentationApi> RelatedPresentations { get; set; }
        public bool IsOwner { get; set; }
    }
}