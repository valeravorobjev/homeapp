using System.Collections.Generic;

namespace HomeApp.Core.Models
{
    /// <summary>
    /// Модель для представления. Список комментариев
    /// </summary>
    public class CommentsModel
    {
        /// <summary>
        /// Список комментариев
        /// </summary>
        public List<UserCommentModel> UserComments { get; set; }
        /// <summary>
        /// Количество комментариев
        /// </summary>
        public int Count { get; set; }
    }
}
