using System.Reflection.Emit;
using Yourslides.Model.Account;
using Yourslides.Utils.DateTimeUtils;

namespace Yourslides.Model {
    public class Presentation {
        public Presentation() {
        }

        public Presentation(long id) {
            Id = id;
        }

        public long Id { get; set; }
        public string Title { get; set; }
        public string OwnerId { get; set; }
        public long WatchCount { get; set; }
        public int SlideCount { get; set; }
        /// <summary>
        /// Presentation time creation in unix timestamp
        /// </summary>
        public int Created { get; set; }
        public User Owner { get; set; }
        public int LogoSlideIndex { get; set; }
        public PresentationVisibility Visibility { get; set; }
        public bool CommentEnable { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
    }
    public enum PresentationVisibility {
        All,
        Link,
        Hide,
        Processing
    }
}