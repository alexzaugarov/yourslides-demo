using System.Collections.Generic;
using Yourslides.Model;
using Yourslides.Model.Api;
using Yourslides.Model.SelectionOptions;

namespace YourSlides.Web.Models {
    public class MainPageModel {
        public IEnumerable<PresentationApi> Presentations { get; set; }
        public PresentationSelectionOptions SearchOptions { get; set; }
    }
}