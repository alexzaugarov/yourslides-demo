using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Yourslides.Model.Api {
    public class CommentApi {
        public long Id { get; set; }
        public long PresentationId { get; set; }
        public string OwnerName { get; set; }
        public bool CanEdit { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Обязательно")]
        [StringLength(500, ErrorMessage = "Длина комментария должна быть не более 500 символов")]
        public string Text { get; set; }
        public int Created { get; set; } 
    }
}