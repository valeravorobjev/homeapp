using HomeApp.Core.Db.Entities;
using HomeApp.Core.Models;

namespace HomeApp.Site.ViewModels
{
    public class UserReviewsViewModel
    {
        public User User { get; set; }
        public Ad Ad { get; set; }
        public CommentsModel CommentList { get; set; }
    }
}
