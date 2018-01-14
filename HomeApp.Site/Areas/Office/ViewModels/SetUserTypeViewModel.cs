using System.ComponentModel.DataAnnotations;

namespace HomeApp.Site.Areas.Office.ViewModels
{
    public class SetUserTypeViewModel
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public bool IsRealtor { get; set; }
    }
}
