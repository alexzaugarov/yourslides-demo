using System.Collections.Generic;
using Yourslides.Model.Api;

namespace YourSlides.Web.Models {
    public class EditViewModel {
        public PresentationApi Presentation { get; set; }
        public List<KeyValuePair<int, string[]>> PreviewSlides { get; set; }
    }
}