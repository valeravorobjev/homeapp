using System.ComponentModel.DataAnnotations;
using HomeApp.Core.Db.Entities.Models.Enums;

namespace HomeApp.Site.Areas.Office.ViewModels
{
    public class SetSocialViewModel
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public UserType UserType { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string GooglePlus { get; set; }
        public string Vk { get; set; }
        public string Ok { get; set; }
        public string Instagram { get; set; }
    }
}
