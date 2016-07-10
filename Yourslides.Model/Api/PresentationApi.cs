using System;
using System.ComponentModel.DataAnnotations;

namespace Yourslides.Model.Api {
    public class PresentationApi {
        public string Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Обязательно")]
        [StringLength(100, ErrorMessage = "Название презентации должно содержать не более 100 символов")]
        public string Title { get; set; }
        public string OwnerName { get; set; }
        public long WatchCount { get; set; }
        public int SlideCount { get; set; }
        /// <summary>
        /// Presentation time creation in unix timestamp
        /// </summary>
        public int Created { get; set; }
        [EnumDataType(typeof(PresentationVisibility))]
        public PresentationVisibility Visibility { get; set; }
        public PresentationStatus Status { get; set; }
        [Required]
        public bool CommentEnable { get; set; }
        [StringLength(1000, ErrorMessage = "Описание должно содержать не более 1000 символов")]

        public string Description { get; set; }
        [Required]
        public string ScreenBackgroundColor { get; set; }
        public string LogoUrl160 { get; set; }
        public string LogoUrl320 { get; set; }
        public string LogoUrl720p { get; set; }
        [Range(1, Int32.MaxValue)]
        public int LogoSlideIndex { get; set; }
        public bool CanEdit { get; set; }
    }
}