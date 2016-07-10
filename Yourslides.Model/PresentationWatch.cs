using System;

namespace Yourslides.Model {
    public class PresentationWatch {
        public long PresentationId { get; set; }
        public string VisitorId { get; set; }
        public Presentation Presentation { get; set; }
        public DateTime WatchDateTime { get; set; }
    }
}