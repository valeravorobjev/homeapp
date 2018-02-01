using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeApp.Core.Db.Entities.Models;
using HomeApp.Core.Db.Entities.Models.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace HomeApp.Site.Areas.Office.ViewModels
{
    public class SetRealtorProfileViewModel
    {
        public string UserId { get; set; }
        public UserType UserType { get; set; }
        public Element Job { get; set; }
        public int FromYear { get; set; }
        public Element WorkRegions { get; set; }
        public List<UnitCategory> RentalProperties { get; set; }
        public List<UnitCategory> EstateSales { get; set; }
        public List<AddService> AddServices { get; set; }
        public Element Description { get; set; }
        public string Site { get; set; }
        public Element Education { get; set; }
    }
}
