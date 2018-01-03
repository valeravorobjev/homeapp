using MongoDB.Bson.Serialization.Attributes;

namespace HomeApp.Core.Db.Entities.Models
{
    /// <summary>
    /// Содержит данные для поисковой оптимизации
    /// </summary>
    public class Seo
    {
        /// <summary>
        /// Лайки
        /// </summary>
        [BsonElement("like_count")]
        public int LikeCount { get; set; }
        /// <summary>
        /// Дизлайки
        /// </summary>
        [BsonElement("dislike_count")]
        public int DislikeCount { get; set; }
        /// <summary>
        /// Рейтинг (расчетное значение, может быть отрицательным
        /// Вес для поисковой оптимизации, чем выше вес, тем выше поднимается объявление
        /// </summary>
        [BsonElement("rating")]
        public double Rating { get; set; }
        /// <summary>
        /// Количество просмотров
        /// </summary>
        [BsonElement("show_count")]
        public int ShowCount { get; set; }
    }
}
