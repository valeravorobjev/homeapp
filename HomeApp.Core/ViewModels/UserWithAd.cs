using HomeApp.Core.Db.Entities;

namespace HomeApp.Core.ViewModels
{
    public class UserWithAd
    {
        public User User { get; set; }
        public Ad Ad { get; set; }
    }
}
