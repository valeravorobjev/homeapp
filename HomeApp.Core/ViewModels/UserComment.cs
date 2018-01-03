using HomeApp.Core.Db.Entities;
using HomeApp.Core.Db.Entities.Models;

namespace HomeApp.Core.ViewModels
{
    /// <summary>
    /// Модель представление. Комментарии пользователя
    /// </summary>
    public class UserComment
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
