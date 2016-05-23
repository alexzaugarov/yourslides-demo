using System.EnterpriseServices.CompensatingResourceManager;

namespace YourSlides.Models {
    public class Presentation {
        public long Id { get; set; }
        public string Title { get; set; }
        public string OwnerName { get; set; }
        public long WatchCount { get; set; }
        public int SlideCount { get; set; }
        public int Created { get; set; }
    }
}