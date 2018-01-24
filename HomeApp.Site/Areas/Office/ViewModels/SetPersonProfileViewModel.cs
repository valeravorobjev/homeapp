using System;
using System.Collections.Generic;
using HomeApp.Core.Db.Entities.Models.Enums;
using MongoDB.Bson;

namespace HomeApp.Site.Areas.Office.ViewModels
{
    public class SetPersonProfileViewModel
    {
        public string UserId { get; set; }
        public UserType UserType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MidName { get; set; }
        public DateTime DateBirth { get; set; }
        public bool? Sex { get; set; }
        public List<string> Phones { get; set; }
        public string Address { get; set; }
    }
}
