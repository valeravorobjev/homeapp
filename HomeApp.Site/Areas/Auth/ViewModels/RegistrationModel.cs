using System.ComponentModel.DataAnnotations;
using HomeApp.Core.Db.Entities.Models.Enums;

namespace HomeApp.Site.Areas.Auth.ViewModels
{
    public class RegistrationModel: LoginModel
    {
        [Required(ErrorMessage = "Пожалуйста, подтвердите пароль!")]
        public string ConfirmPassword { get; set; }
        public Language Language { get; set; }
    }
}
