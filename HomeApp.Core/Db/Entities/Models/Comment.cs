using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HomeApp.Core.Db.Entities.Models
{
    /// <summary>
    /// Отзыв
    /// </summary>
    public class Comment
    {
        /// <summary>
        /// Оценка
        /// </summary>
        [BsonElement("eval")]
        public byte Eval { get; set; }
        /// <summary>
        /// Текст отзыва
        /// </summary>
        [BsonElement("text")]
        public string Text { get; set; }
        /// <summary>
        /// Пользователь, который оставил отзыв
        /// </summary>
        [BsonElement("user_id")]
        public ObjectId UserId { get; set; }
        /// <summary>
        /// Дата отзыва
        /// </summary>
        [BsonElement("date")]
        public DateTime Date { get; set; }
    }
}
