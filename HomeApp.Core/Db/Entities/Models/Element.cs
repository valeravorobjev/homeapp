using MongoDB.Bson.Serialization.Attributes;

namespace HomeApp.Core.Db.Entities.Models
{
    /// <summary>
    /// Элемент с локализованными строками
    /// </summary>
    public class Element
    {
        /// <summary>
        /// Английский язык
        /// </summary>
        [BsonIgnoreIfNull]
        [BsonElement("en")]
        public string En { get; set; }
        /// <summary>
        /// Русский язык
        /// </summary>
        [BsonIgnoreIfNull]
        [BsonElement("ru")]
        public string Ru { get; set; }
    }
}
