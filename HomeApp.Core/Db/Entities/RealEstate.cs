using System;
using HomeApp.Core.Db.Entities.Models;
using HomeApp.Core.Db.Entities.Models.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HomeApp.Core.Db.Entities
{
    /// <summary>
    /// Недвижимость
    /// </summary>
    [BsonIgnoreExtraElements]
    public class RealEstate
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [BsonId]
        public ObjectId Id { get; set; }
        /// <summary>
        /// Дата публикации
        /// </summary>
        [BsonElement("publish_date")]
        public DateTime PublishDate { get; set; }
        /// <summary>
        /// Срок окончания публикации
        /// </summary>
        [BsonElement("expiration_date")]
        public DateTime ExpirationDate { get; set; }
        /// <summary>
        /// Статус объявления
        /// </summary>
        [BsonElement("real_estate_status")]
        public RealEstateStatus RealEstateStatus { get; set; }
        /// <summary>
        /// Идентификатор пользователя, который продает или сдает данную недвижимость
        /// </summary>
        [BsonElement("user_id")]
        public ObjectId UserId { get; set; }
        /// <summary>
        /// Объект недвижимости
        /// </summary>
        [BsonElement("unit")]
        public UnitBase Unit { get; set; }

    }
}
