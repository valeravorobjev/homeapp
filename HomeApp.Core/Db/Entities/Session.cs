using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HomeApp.Core.Db.Entities
{
    /// <summary>
    /// Сессия
    /// </summary>
    [BsonIgnoreExtraElements]
    public class Session
    {
        /// <summary>
        /// Идентификатор сессии
        /// </summary>
        [BsonId]
        public ObjectId Id { get; set; }
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        [BsonElement("user_id")]
        public ObjectId UserId { get; set; }
        /// <summary>
        /// Секретный ключ
        /// </summary>
        [BsonElement("token")]
        public string Token { get; set; }
        /// <summary>
        /// Дата окончания сессии
        /// </summary>
        [BsonElement("expiration_date")]
        public DateTime ExpirationDate { get; set; }
    }
}
