using System;
using System.Collections.Generic;
using Yourslides.Model.Account;

namespace Yourslides.Model {
    public class Presentation : IEntity {
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
        public DateTime CreatedDateTime { get; set; }
        public User Owner { get; set; }
        public int LogoSlideIndex { get; set; }
        public PresentationVisibility Visibility { get; set; }
        public PresentationStatus Status { get; set; }
        public bool CommentEnable { get; set; }
        public string Description { get; set; }
        public string ScreenBackgroundColor { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
    public enum PresentationVisibility {
        All,
        Link,
        Hide,
        None
    }
    public enum PresentationStatus {
        Ready,
        Processing,
        Queue,
        None
    }
}