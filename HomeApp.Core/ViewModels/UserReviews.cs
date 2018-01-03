using HomeApp.Core.Db.Entities;

namespace HomeApp.Core.ViewModels
{
    public class UserReviews
    {
        public User User { get; set; }
        public Ad Ad { get; set; }
        public CommentList CommentList { get; set; }
    }
}
