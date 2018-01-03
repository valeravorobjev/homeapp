using HomeApp.Core.Db.Entities;

namespace HomeApp.Core.ViewModels
{
    public class UserOverview
    {
        public User User { get; set; }
        public Ad Ad { get; set; }
        public int SoldRentEstateCount { get; set; }
        public int ActiveRentEstateCount { get; set; }
    }
}
