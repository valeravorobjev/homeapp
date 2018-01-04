using System.ComponentModel.DataAnnotations;

namespace HomeApp.Site.Areas.Auth.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Пожалуйста, укажите почту!")]
        [EmailAddress(ErrorMessage = "Электронная почта указана не верно")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Пожалуйста, укажите пароль!")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Минимальная длинна пароля 6 символов, максимальная 100")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
