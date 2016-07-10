using System.ComponentModel.DataAnnotations;

namespace Yourslides.Model.SelectionOptions {
    public class CommentsSelectionOptions : SelectionOptionsBase {
        public CommentsSelectionOptions() {
            Count = 1000;
        }

        [Required]
        public long PresentationId { get; set; }
        public bool SortAscending { get; set; }
    }
}