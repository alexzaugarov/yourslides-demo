using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace YourSlides.Web.Models.Auth {
    public class SignupPage {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Обязательное поле")]
        [DataType(DataType.Text)]
        [DisplayName("Имя пользователя")]
        [StringLength(30, ErrorMessage = "Имя пользователя должно содержать от 5 до 30 символов", MinimumLength = 5)]
        [Remote("CheckLogin", "Auth", HttpMethod = "POST", ErrorMessage = "Имя пользователя уже используется")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Имя пользователя может содержать только латинские буквы и цифры")]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Обязательное поле")]
        [DataType(DataType.Password)]
        [DisplayName("Пароль")]
        [StringLength(int.MaxValue, MinimumLength = 6, ErrorMessage = "Пароль должен содержать 6 и более символов")]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Обязательное поле")]
        [DataType(DataType.Password)]
        [DisplayName("Еще раз пароль")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Пароли должны совпадать")]
        public string ConfirmPassword { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Обязательное поле")]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Электронная почта")]
        public string Email { get; set; }
    }
}