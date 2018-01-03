using System;
using HomeApp.Core.Db.Entities.Models.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace HomeApp.Core.Db.Entities.Models
{
    /// <summary>
    /// Квартира
    /// </summary>
    [BsonIgnoreExtraElements]
    public class Flat: Room
    {
        /// <summary>
        /// Срок сдачи дома
        /// </summary>
        [BsonElement("delivery_date")]
        [BsonIgnoreIfDefault]
        public DateTime DeliveryDate { get; set; }
        /// <summary>
        /// Тип возраста недвижимости
        /// </summary>
        [BsonElement("old_type")]
        [BsonIgnoreIfDefault]
        public OldType OldType { get; set; }
        /// <summary>
        /// Изображение схемы
        /// </summary>
        [BsonElement("plan_path")]
        [BsonIgnoreIfNull]
        public string FloorPlanPath { get; set; }
    }
}
