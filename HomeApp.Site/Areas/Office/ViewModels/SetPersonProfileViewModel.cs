using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HomeApp.Core.Db.Entities.Models.Enums;
using MongoDB.Bson;

namespace HomeApp.Site.Areas.Office.ViewModels
{
    public class SetPersonProfileViewModel
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public UserType UserType { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string MidName { get; set; }
        public DateTime? DateBirth { get; set; }
        public bool? Sex { get; set; }
        public List<string> Phones { get; set; }
        public string Address { get; set; }
    }
}
