using HomeApp.Core.Db.Entities;
using HomeApp.Core.Db.Entities.Models;

namespace HomeApp.Core.Models
{
    /// <summary>
    /// Модель представление. Комментарии пользователя
    /// </summary>
    public class UserCommentModel
    {
        /// <summary>
        /// Пользователь
        /// </summary>
        public User User { get; set; }
        /// <summary>
        /// Комментарий
        /// </summary>
        public Comment Comment { get; set; }
    }
}
