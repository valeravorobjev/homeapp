using System.Collections.Generic;

namespace HomeApp.Core.ViewModels
{
    /// <summary>
    /// Модель для представления. Список комментариев
    /// </summary>
    public class CommentList
    {
        /// <summary>
        /// Список комментариев
        /// </summary>
        public List<UserComment> UserComments { get; set; }
        /// <summary>
        /// Количество комментариев
        /// </summary>
        public int Count { get; set; }
    }
}
