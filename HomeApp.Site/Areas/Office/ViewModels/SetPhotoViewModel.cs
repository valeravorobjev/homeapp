using System.ComponentModel.DataAnnotations;
using HomeApp.Core.Db.Entities.Models.Enums;
using Microsoft.AspNetCore.Http;

namespace HomeApp.Site.Areas.Office.ViewModels
{
    public class SetPhotoViewModel
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public UserType UserType { get; set; }
        public IFormFile File { get; set; }
    }
}
