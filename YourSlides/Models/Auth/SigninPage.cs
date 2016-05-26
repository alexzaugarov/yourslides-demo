using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YourSlides.Models.Auth {
    public class SigninPage {
        [DisplayName("Имя пользователя")]
        [Required]
        [DataType(DataType.Text)]
        public string UserName { get; set; }
        [DisplayName("Пароль")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DisplayName("Запомнить")]
        [Required]
        public bool IsPersistent { get; set; }
    }
}