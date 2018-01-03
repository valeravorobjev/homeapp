using HomeApp.Core.Db.Entities;
using HomeApp.Core.Models;

namespace HomeApp.Site.ViewModels
{
    public class UserPropertiesViewModel
    {
        public User User { get; set; }
        public Ad Ad { get; set; }
        public RealEstatesModel RealEstateList { get; set; }
    }
}
