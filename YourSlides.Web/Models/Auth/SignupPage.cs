using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YourSlides.Web.Models.Auth {
    public class SignupPage {
        [Required]
        [DataType(DataType.Text)]
        [DisplayName("Логин")]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Пароль")]
        public string Password { get; set; } 

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Еще раз пароль")]
        public string ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Почтовый адрес")]
        public string Email { get; set; }


    }
}