using MongoDB.Bson.Serialization.Attributes;

namespace HomeApp.Core.Db.Entities.Models
{
    /// <summary>
    /// Социальные сети
    /// </summary>
    public class SocialMedia
    {
        /// <summary>
        /// Фейсбук
        /// </summary>
        [BsonElement("facebook")]
        [BsonIgnoreIfNull]
        public string Facebook { get; set; }

        /// <summary>
        /// Твиттер
        /// </summary>
        [BsonElement("twitter")]
        [BsonIgnoreIfNull]
        public string Twitter { get; set; }

        /// <summary>
        /// Гугл +
        /// </summary>
        [BsonElement("google_plus")]
        [BsonIgnoreIfNull]
        public string GooglePlus { get; set; }

        /// <summary>
        /// Вконтакте
        /// </summary>
        [BsonElement("vk")]
        [BsonIgnoreIfNull]
        public string Vk { get; set; }

        /// <summary>
        /// Одноклассники
        /// </summary>
        [BsonElement("ok")]
        [BsonIgnoreIfNull]
        public string Ok { get; set; }

        /// <summary>
        /// Инстаграм
        /// </summary>
        [BsonElement("instagram")]
        [BsonIgnoreIfNull]
        public string Instagram { get; set; }
    }
}
