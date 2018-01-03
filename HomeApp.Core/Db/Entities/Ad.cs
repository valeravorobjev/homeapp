using System;
using HomeApp.Core.Db.Entities.Models.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HomeApp.Core.Db.Entities
{
    /// <summary>
    /// Рекламные компании
    /// </summary>
    [BsonIgnoreExtraElements]
    public class Ad
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [BsonId]
        public ObjectId AdId { get; set; }
        /// <summary>
        /// Название рекламной компании
        /// </summary>
        [BsonElement("name")]
        public string Name { get; set; }
        /// <summary>
        /// Тип рекламной компании
        /// </summary>
        [BsonElement("ad_type")]
        public AdType AdType { get; set; }
        /// <summary>
        /// Статус рекламной компании
        /// </summary>
        [BsonElement("ad_status")]
        public AdStatus AdStatus { get; set; }
        /// <summary>
        /// Описание
        /// </summary>
        [BsonElement("description")]
        public string Description { get; set; }
        /// <summary>
        /// Стоимость
        /// </summary>
        [BsonElement("cost")]
        public float Cost { get; set; }
        /// <summary>
        /// Срок действия
        /// </summary>
        [BsonElement("duration")]
        public DateTime Duration { get; set; }
    }
}
