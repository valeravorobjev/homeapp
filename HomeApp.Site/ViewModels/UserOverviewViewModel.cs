using HomeApp.Core.Db.Entities;

namespace HomeApp.Site.ViewModels
{
    public class UserOverviewViewModel
    {
        public User User { get; set; }
        public Ad Ad { get; set; }
        public int SoldRentEstateCount { get; set; }
        public int ActiveRentEstateCount { get; set; }
    }
}
