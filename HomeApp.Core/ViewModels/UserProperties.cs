using HomeApp.Core.Db.Entities;

namespace HomeApp.Core.ViewModels
{
    public class UserProperties
    {
        public User User { get; set; }
        public Ad Ad { get; set; }
        public RealEstateList RealEstateList { get; set; }
    }
}
