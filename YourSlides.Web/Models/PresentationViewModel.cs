using Yourslides.Model;

namespace YourSlides.Web.Models {
    public class PresentationViewModel {
        public string Id { get; set; }
        public string Title { get; set; }
        public string OwnerName { get; set; }
        public long WatchCount { get; set; }
        public int SlideCount { get; set; }
        /// <summary>
        /// Presentation time creation in unix timestamp
        /// </summary>
        public int Created { get; set; }
        public int LogoSlideIndex { get; set; }
        public PresentationVisibility Visibility { get; set; }
        public bool CommentEnable { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
    }
}