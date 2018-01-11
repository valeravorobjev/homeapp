using System.ComponentModel.DataAnnotations;

namespace HomeApp.Site.Areas.Auth.ViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }
    }
}
